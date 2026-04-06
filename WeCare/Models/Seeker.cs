using System.ComponentModel.DataAnnotations;

namespace WeCare.Models;

public class Seeker
{
    public int SeekerId { get; set; }
    public int UserId { get; set; }

    [MaxLength(10)]
    public string? BloodGroupNeeded { get; set; }

    [MaxLength(120)]
    public string? Location { get; set; }

    public User? User { get; set; }
}
