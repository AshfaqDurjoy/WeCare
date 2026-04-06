namespace WeCare.ViewModels;

public class EventDisplayMV
{
    public int EventId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public TimeSpan EventTime { get; set; }
    public string Location { get; set; } = string.Empty;
    public string? MapLink { get; set; }
    public string? ImagePath { get; set; }
    public bool HasResponded { get; set; }
}
