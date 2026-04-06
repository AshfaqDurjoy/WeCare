using System.ComponentModel.DataAnnotations;

namespace WeCare.Models;

public class EventResponseTable
{
    public int ResponseId { get; set; }

    [Required]
    public int EventId { get; set; }

    [Required]
    public int UserId { get; set; }

    public DateTime ResponseDate { get; set; }

    public EventTable? Event { get; set; }
    public User? User { get; set; }
}
