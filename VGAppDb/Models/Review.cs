namespace VGAppDb.Models;

public class Review
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
    public float Rating { get; set; }

    public required Game Game { get; set; }
}