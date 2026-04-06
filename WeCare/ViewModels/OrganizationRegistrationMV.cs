using System.ComponentModel.DataAnnotations;

namespace WeCare.ViewModels;

public class OrganizationRegistrationMV
{
    [Required]
    public string OrganizationType { get; set; } = "Hospital";

    [Required, MaxLength(150)]
    public string OrganizationName { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string Address { get; set; } = string.Empty;

    [Required, Phone, MaxLength(20)]
    public string Phone { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required, MinLength(6), DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required, Compare(nameof(Password)), DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = string.Empty;
}
