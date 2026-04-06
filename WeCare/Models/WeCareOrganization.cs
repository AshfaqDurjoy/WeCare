using System.ComponentModel.DataAnnotations;

namespace WeCare.Models;

public class WeCareOrganization
{
    public int WeCareOrganizationId { get; set; }
    public int UserId { get; set; }

    [Required, MaxLength(150)]
    public string OrganizationName { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string Address { get; set; } = string.Empty;

    [Required, Phone, MaxLength(20)]
    public string Phone { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    public User? User { get; set; }
}
