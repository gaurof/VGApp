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
    public required string PosterUrl { get; set; }

    public List<Review>? Reviews { get; set; }

}
