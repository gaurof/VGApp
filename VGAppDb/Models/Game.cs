using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGAppDb.Models;

public class Game
{
    public Guid Id { get; set; }
    [Required]
    public required string Name { get; set; }
    public string? Description { get; set; }
    [Required]
    public required decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
    [Required]
    public required string PosterUrl { get; set; }     //600×900
    [Required]
    public required string BackgroundUrl { get; set; } //1920×620

    public List<Review>? Reviews { get; set; }

}
