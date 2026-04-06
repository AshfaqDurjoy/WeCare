using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeCare.Data;
using WeCare.Models;
using WeCare.ViewModels;

namespace WeCare.Controllers;

public class EventController : Controller
{
    private readonly WeCareDbContext _db;
    private readonly IWebHostEnvironment _environment;

    public EventController(WeCareDbContext db, IWebHostEnvironment environment)
    {
        _db = db;
        _environment = environment;
    }

    public IActionResult Create()
    {
        if (HttpContext.Session.GetInt32("UserID") == null)
        {
            return RedirectToAction("Login", "Home");
        }

        return View(new EventCreateMV { EventDate = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EventCreateMV model)
    {
        var userId = HttpContext.Session.GetInt32("UserID");
        if (userId == null)
        {
            return RedirectToAction("Login", "Home");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            var imagePath = await SaveImageAsync(model.ImageFile);

            var newEvent = new EventTable
            {
                EventId = GetNextId(_db.Events.Select(e => e.EventId)),
                Title = model.Title,
                Description = model.Description,
                EventDate = model.EventDate.Date,
                EventTime = model.EventTime,
                Location = model.Location,
                MapLink = model.MapLink,
                ImagePath = imagePath,
                CreatedByUserId = userId.Value,
                CreatedAt = DateTime.UtcNow
            };

            _db.Events.Add(newEvent);
            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            return RedirectToAction("AllCampaigns", "Home");
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            ModelState.AddModelError(string.Empty, "Something went wrong.");
            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Respond(int eventId)
    {
        var userId = HttpContext.Session.GetInt32("UserID");
        if (userId == null)
        {
            return Json(new { success = false, message = "Login required to respond to this event." });
        }

        try
        {
            if (_db.EventResponses.Any(r => r.EventId == eventId && r.UserId == userId.Value))
            {
                return Json(new { success = false, message = "You have already responded." });
            }

            _db.EventResponses.Add(new EventResponseTable
            {
                ResponseId = GetNextId(_db.EventResponses.Select(r => r.ResponseId)),
                EventId = eventId,
                UserId = userId.Value,
                ResponseDate = DateTime.UtcNow
            });

            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Response recorded." });
        }
        catch (Exception)
        {
            return Json(new { success = false, message = "Something went wrong." });
        }
    }

    private async Task<string?> SaveImageAsync(IFormFile? imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
        {
            return null;
        }

        var folder = Path.Combine(_environment.WebRootPath, "Content", "EventImages");
        Directory.CreateDirectory(folder);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
        var filePath = Path.Combine(folder, fileName);

        await using var stream = System.IO.File.Create(filePath);
        await imageFile.CopyToAsync(stream);

        return $"/Content/EventImages/{fileName}";
    }

    private static int GetNextId(IEnumerable<int> existingIds)
    {
        return existingIds.Any() ? existingIds.Max() + 1 : 1;
    }
}
