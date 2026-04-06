using System.ComponentModel.DataAnnotations;

namespace WeCare.Models;

public class User
{
    public int UserId { get; set; }

    [Required, MaxLength(100)]
    public string UserName { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(150)]
    public string EmailAddress { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string PasswordHash { get; set; } = string.Empty;

    public int AccountStatusId { get; set; }
    public int UserTypeId { get; set; }

    public AccountStatus? AccountStatus { get; set; }
    public UserType? UserType { get; set; }

    public Donor? DonorProfile { get; set; }
    public Seeker? SeekerProfile { get; set; }
    public Hospital? HospitalProfile { get; set; }
    public WeCareOrganization? OrganizationProfile { get; set; }
}
