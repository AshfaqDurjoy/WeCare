using System.ComponentModel.DataAnnotations;

namespace WeCare.Models;

public class UserType
{
    public int UserTypeId { get; set; }

    [Required, MaxLength(50)]
    public string TypeName { get; set; } = string.Empty;

    public ICollection<User> Users { get; set; } = new List<User>();
}