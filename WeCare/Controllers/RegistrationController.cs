using Microsoft.AspNetCore.Mvc;
using WeCare.Data;
using WeCare.Models;
using WeCare.Security;
using WeCare.ViewModels;

namespace WeCare.Controllers;

public class RegistrationController : Controller
{
    private readonly WeCareDbContext _db;

    public RegistrationController(WeCareDbContext db)
    {
        _db = db;
    }

    public IActionResult Register()
    {
        return RedirectToAction(nameof(SelectType));
    }

    public IActionResult SelectType()
    {
        return View();
    }

    public IActionResult Individual()
    {
        return View(new IndividualRegistrationMV { RegistrationType = "Donor" });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Individual(IndividualRegistrationMV model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            var userTypeId = model.RegistrationType == "Seeker" ? 2 : 1;
            var user = new User
            {
                UserId = GetNextId(_db.Users.Select(u => u.UserId)),
                UserName = model.FullName,
                EmailAddress = model.Email,
                PasswordHash = PasswordHasher.HashPassword(model.Password),
                AccountStatusId = 1,
                UserTypeId = userTypeId
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            _db.Donors.Add(new Donor
            {
                DonorId = GetNextId(_db.Donors.Select(d => d.DonorId)),
                UserId = user.UserId,
                FullName = model.FullName,
                Address = model.Address,
                JobStatus = model.JobStatus,
                ContactNo = model.ContactNo,
                BloodGroup = model.BloodGroup,
                LastDonationDate = model.LastDonationDate
            });

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            return RedirectToAction("Login", "Home");
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
            return View(model);
        }
    }

    public IActionResult Organization()
    {
        return View(new OrganizationRegistrationMV());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Organization(OrganizationRegistrationMV model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            var isHospital = model.OrganizationType == "Hospital";
            var userTypeId = isHospital ? 4 : 5;
            var user = new User
            {
                UserId = GetNextId(_db.Users.Select(u => u.UserId)),
                UserName = model.OrganizationName,
                EmailAddress = model.Email,
                PasswordHash = PasswordHasher.HashPassword(model.Password),
                AccountStatusId = 1,
                UserTypeId = userTypeId
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            if (isHospital)
            {
                _db.Hospitals.Add(new Hospital
                {
                    HospitalId = GetNextId(_db.Hospitals.Select(h => h.HospitalId)),
                    UserId = user.UserId,
                    HospitalName = model.OrganizationName,
                    Address = model.Address,
                    Phone = model.Phone,
                    Email = model.Email
                });
            }
            else
            {
                _db.WeCareOrganizations.Add(new WeCareOrganization
                {
                    WeCareOrganizationId = GetNextId(_db.WeCareOrganizations.Select(o => o.WeCareOrganizationId)),
                    UserId = user.UserId,
                    OrganizationName = model.OrganizationName,
                    Address = model.Address,
                    Phone = model.Phone,
                    Email = model.Email
                });
            }

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            return RedirectToAction("Login", "Home");
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
            return View(model);
        }
    }

    private static int GetNextId(IEnumerable<int> existingIds)
    {
        return existingIds.Any() ? existingIds.Max() + 1 : 1;
    }
}
