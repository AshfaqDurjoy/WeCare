using System.ComponentModel.DataAnnotations;

namespace WeCare.Models;

public class Donor
{
    public int DonorId { get; set; }
    public int? UserId { get; set; }

    [Required, MaxLength(120)]
    public string FullName { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? Gender { get; set; }

    [Required, MaxLength(200)]
    public string Address { get; set; } = string.Empty;

    [Required, MaxLength(40)]
    public string JobStatus { get; set; } = string.Empty;

    [Required, Phone, MaxLength(20)]
    public string ContactNo { get; set; } = string.Empty;

    [Required, MaxLength(10)]
    public string BloodGroup { get; set; } = string.Empty;

    public DateTime? LastDonationDate { get; set; }

    [MaxLength(150)]
    public string? ContactInfo { get; set; }

    public User? User { get; set; }
}
