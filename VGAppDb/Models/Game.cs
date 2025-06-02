using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace VGAppDb.Models;

public class Game
{
    [Key]
    [Required]
    public required string Name { get; set; }

    public string? Description { get; set; }

    [Required]
    public required decimal PriceUSD { get; set; }

    public int ReleaseYear { get; set; }

    [Required]
    public required string PosterUrl { get; set; }
        //600×900

    [Required]
    public required string BackgroundUrl { get; set; } 
        //1920×620

    [Required]
    public required string LogoUrl { get; set; }



    public List<Review> Reviews { get; set; } = [];


    public override bool Equals(object? obj)
    {
        if (obj is not Game || obj is null)
            return false;
        return Equals((Game)obj);
    }
    public bool Equals(Game game)
    {
        if (Name == game.Name) 
            return true;
        return false;
    }

    public override int GetHashCode() => HashCode.Combine(Name);
}
