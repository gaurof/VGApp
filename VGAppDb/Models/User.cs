using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VGAppDb.Models;

public class User : IdentityUser
{
    public DateTime TimeCreated { get; set; } = DateTime.UtcNow;

    public List<Review> Reviews { get; set; } = [];
    public List<Review> LikedReviews { get; set; } = [];
    public List<Game> GamesPlayed { get; set; } = [];

    public bool HasReviewed(Game game) =>
        game.Reviews.Any(r => r.User == this);
    public bool HasPlayed(Game game) =>
        GamesPlayed.Contains(game);
}

