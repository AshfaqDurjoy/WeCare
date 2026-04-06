using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace WeCare.ViewModels;

public class EventCreateMV
{
    [Required, MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(1200)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public DateTime EventDate { get; set; }

    [Required]
    public TimeSpan EventTime { get; set; }

    [Required, MaxLength(200)]
    public string Location { get; set; } = string.Empty;

    [Url, MaxLength(500)]
    public string? MapLink { get; set; }

    public IFormFile? ImageFile { get; set; }
}
