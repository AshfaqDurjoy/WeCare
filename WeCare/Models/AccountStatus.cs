using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeCare.Models;

public class AccountStatus
{
    public int AccountStatusId { get; set; }

    [Required, MaxLength(50)]
    public string StatusName { get; set; } = string.Empty;

    public ICollection<User> Users { get; set; } = new List<User>();
}