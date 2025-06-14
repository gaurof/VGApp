using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VGAppDb.Models;

public class User : IdentityUser
{
    public List<Review> Reviews { get; set; } = [];
    public List<Review> LikedReviews { get; set; } = [];
    public List<Game> GamesPlayed { get; set; } = [];
    public DateTime TimeCreated { get; set; } = DateTime.Now;
}

