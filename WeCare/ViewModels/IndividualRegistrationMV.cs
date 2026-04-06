using System.ComponentModel.DataAnnotations;

namespace WeCare.ViewModels;

public class IndividualRegistrationMV
{
    [Required, MaxLength(120)]
    public string FullName { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string Address { get; set; } = string.Empty;

    [Required, MaxLength(40)]
    public string JobStatus { get; set; } = string.Empty;

    [Required, Phone, MaxLength(20)]
    public string ContactNo { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required, MinLength(6), DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required, Compare(nameof(Password)), DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required, MaxLength(10)]
    public string BloodGroup { get; set; } = string.Empty;

    public DateTime? LastDonationDate { get; set; }

    [Required]
    public string RegistrationType { get; set; } = "Donor";
}
