using System.ComponentModel.DataAnnotations;

namespace WeCare.Models;

public class EventTable
{
    public int EventId { get; set; }

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

    [MaxLength(500)]
    public string? MapLink { get; set; }

    [MaxLength(250)]
    public string? ImagePath { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public User? CreatedByUser { get; set; }
    public List<EventResponseTable> Responses { get; set; } = new();
}
