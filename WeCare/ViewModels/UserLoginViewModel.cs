using System.ComponentModel.DataAnnotations;

namespace WeCare.ViewModels;

public class UserLoginViewModel
{
    [Required, EmailAddress]
    public string EmailAddress { get; set; } = string.Empty;

    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; }
}