using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VGAppDb.Models;

public class Review
{
    public int Id { get; set; }

    [DataType(DataType.MultilineText)]
    public string? Text { get; set; }

    public float? Rating { get; set; }
    public DateTime PublicationTime { get; set; } = DateTime.UtcNow;

    public required Game Game { get; set; }
    public required User User { get; set; }
    public List<User> UsersThatLiked { get; set; } = [];
    [NotMapped]
    public int LikesCount => UsersThatLiked.Count;
}