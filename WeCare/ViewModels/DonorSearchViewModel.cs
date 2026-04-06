using WeCare.Models;

namespace WeCare.ViewModels;

public class DonorSearchViewModel
{
    public string? SelectedBloodGroup { get; set; }
    public List<string> BloodGroups { get; set; } = new();
    public List<Donor> Results { get; set; } = new();
}
