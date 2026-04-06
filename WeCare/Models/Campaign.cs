using System.ComponentModel.DataAnnotations;

namespace WeCare.Models;

public class Campaign
{
    public int CampaignId { get; set; }

    [Required, MaxLength(150)]
    public string CampaignName { get; set; } = string.Empty;

    public DateTime CampaignDate { get; set; }

    [Required, MaxLength(150)]
    public string Location { get; set; } = string.Empty;

    [MaxLength(150)]
    public string? Organizer { get; set; }
}
