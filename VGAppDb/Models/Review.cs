namespace VGAppDb.Models;

public class Review
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
    public float? Rating { get; set; } // From 0.5 to 5 stars

    public required Game Game { get; set; }
}