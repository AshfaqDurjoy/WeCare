using WeCare.Models;

namespace WeCare.ViewModels;

public class DashboardViewModel
{
    public string UserTypeName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public Donor? DonorProfile { get; set; }
    public Seeker? SeekerProfile { get; set; }
    public Hospital? HospitalProfile { get; set; }
    public WeCareOrganization? OrganizationProfile { get; set; }
    public DateTime? LastDonationDate { get; set; }
    public List<Campaign> UpcomingCampaigns { get; set; } = new();
    public List<EventDisplayMV> ParticipatedEvents { get; set; } = new();
}
