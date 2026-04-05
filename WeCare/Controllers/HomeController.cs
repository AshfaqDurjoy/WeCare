/*using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeCare.Data;
using WeCare.Security;
using WeCare.ViewModels;

namespace WeCare.Controllers;

public class HomeController : Controller
{
    private readonly WeCareDbContext _db;

    public HomeController(WeCareDbContext db)
    {
        _db = db;
    }

    public IActionResult MainHome()
    {
        var model = new MainHomeViewModel
        {
            UpcomingCampaigns = _db.Campaigns
                .Where(c => c.CampaignDate >= DateTime.Today)
                .OrderBy(c => c.CampaignDate)
                .ToList()
        };

        return View(model);
    }

    public IActionResult Login()
    {
        return View(new UserLoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(UserLoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = _db.Users
            .FirstOrDefault(u => u.EmailAddress == model.EmailAddress);

        if (user == null || !PasswordHasher.VerifyPassword(model.Password, user.PasswordHash))
        {
            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return View(model);
        }

        if (user.AccountStatusId != 1)
        {
            ModelState.AddModelError(string.Empty, "Your account is not active.");
            return View(model);
        }

        HttpContext.Session.SetInt32("UserID", user.UserId);
        HttpContext.Session.SetInt32("UserType", user.UserTypeId);
        HttpContext.Session.SetString("UserName", user.UserName);

        return RedirectToAction("Dashboard");
    }

    public IActionResult Dashboard()
    {
        var userId = HttpContext.Session.GetInt32("UserID");
        if (userId == null)
        {
            return RedirectToAction("Login");
        }

        var user = _db.Users.FirstOrDefault(u => u.UserId == userId);

        if (user == null)
        {
            return RedirectToAction("Login");
        }

        var model = new DashboardViewModel
        {
            UserTypeName = _db.UserTypes.FirstOrDefault(t => t.UserTypeId == user.UserTypeId)?.TypeName ?? "",
            UserName = user.UserName,
            DonorProfile = _db.Donors.FirstOrDefault(d => d.UserId == user.UserId),
            SeekerProfile = _db.Seekers.FirstOrDefault(s => s.UserId == user.UserId),
            HospitalProfile = _db.Hospitals.FirstOrDefault(h => h.UserId == user.UserId),
            OrganizationProfile = _db.WeCareOrganizations.FirstOrDefault(o => o.UserId == user.UserId),
            UpcomingCampaigns = _db.Campaigns
                .Where(c => c.CampaignDate >= DateTime.Today)
                .OrderBy(c => c.CampaignDate)
                .ToList()
        };

        model.LastDonationDate = model.DonorProfile?.LastDonationDate;
        model.ParticipatedEvents = _db.EventResponses
            .Where(r => r.UserId == user.UserId)
            .Join(_db.Events, response => response.EventId, ev => ev.EventId, (response, ev) => new EventDisplayMV
            {
                EventId = ev.EventId,
                Title = ev.Title,
                Description = ev.Description,
                EventDate = ev.EventDate,
                EventTime = ev.EventTime,
                Location = ev.Location,
                MapLink = ev.MapLink,
                ImagePath = ev.ImagePath,
                HasResponded = true
            })
            .OrderByDescending(ev => ev.EventDate)
            .ToList();

        return View(model);
    }

    public IActionResult Campaigns()
    {
        return RedirectToAction(nameof(AllCampaigns));
    }

    public IActionResult AllCampaigns()
    {
        var userId = HttpContext.Session.GetInt32("UserID");
        var upcomingEvents = _db.Events
            .Where(e => e.EventDate >= DateTime.Today)
            .OrderBy(e => e.EventDate)
            .ThenBy(e => e.EventTime)
            .Select(e => new EventDisplayMV
            {
                EventId = e.EventId,
                Title = e.Title,
                Description = e.Description,
                EventDate = e.EventDate,
                EventTime = e.EventTime,
                Location = e.Location,
                MapLink = e.MapLink,
                ImagePath = e.ImagePath,
                HasResponded = userId != null && _db.EventResponses.Any(r => r.EventId == e.EventId && r.UserId == userId)
            })
            .ToList();

        return View(upcomingEvents);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("MainHome");
    }

    public IActionResult Error()
    {
        return View();
    }
}*/