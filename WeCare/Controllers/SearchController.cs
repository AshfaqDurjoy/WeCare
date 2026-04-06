using Microsoft.AspNetCore.Mvc;
using WeCare.Data;
using WeCare.ViewModels;

namespace WeCare.Controllers;

public class SearchController : Controller
{
    private static readonly List<string> AvailableBloodGroups = new()
    {
        "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-"
    };

    private readonly WeCareDbContext _db;

    public SearchController(WeCareDbContext db)
    {
        _db = db;
    }

    public IActionResult Index(string? bloodGroup)
    {
        var normalizedGroup = string.IsNullOrWhiteSpace(bloodGroup) ? null : bloodGroup.Trim().ToUpperInvariant();

        var results = _db.Donors
            .Where(d => string.IsNullOrWhiteSpace(normalizedGroup) || d.BloodGroup.ToUpperInvariant() == normalizedGroup)
            .OrderBy(d => d.FullName)
            .ToList();

        var model = new DonorSearchViewModel
        {
            SelectedBloodGroup = normalizedGroup,
            BloodGroups = AvailableBloodGroups,
            Results = results
        };

        return View(model);
    }
}
