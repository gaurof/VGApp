using System.ComponentModel.DataAnnotations;

namespace VGAppDb.Models;

public class Review
{
    public int Id { get; set; }

    [DataType(DataType.MultilineText)]
    public string? Text { get; set; }

    public float? Rating { get; set; } // From 0.5 to 5 stars
    public DateTime PublicationTime { get; set; } = DateTime.Now;

    public required Game Game { get; set; }
    public required User User { get; set; }
    public List<User> UsersThatLiked { get; set; } = [];
    public int LikesCount => UsersThatLiked.Count;
}