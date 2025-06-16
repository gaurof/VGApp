using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Reflection.Emit;
using VGAppDb.Models;

namespace VGAppDb;

public class VGAppDbContext : IdentityDbContext<User>
{
    public VGAppDbContext(DbContextOptions<VGAppDbContext> options)
        : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Review>()
        .HasMany(r => r.UsersThatLiked)
        .WithMany(u => u.LikedReviews)
        .UsingEntity<Dictionary<string, object>>(
            "ReviewLikes",
            j => j.HasOne<User>().WithMany(),
            j => j.HasOne<Review>().WithMany()
        );

        builder.Entity<Game>()
        .HasMany(r => r.UsersThatPlayed)
        .WithMany(u => u.GamesPlayed)
        .UsingEntity<Dictionary<string, object>>(
            "GameUsers",
            j => j.HasOne<User>().WithMany(),
            j => j.HasOne<Game>().WithMany()
        );

        builder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews);

        builder.Entity<Game>().HasData(
            new Game()
            {
                Name = "Minecraft",
                Description = "Also try terraria!",
                PriceUSD = 20,
                ReleaseYear = 2011,
                PosterUrl = "https://cdn2.steamgriddb.com/thumb/782c68199db381ee34a277258c28c89c.jpg",
                BackgroundUrl = "https://cdn2.steamgriddb.com/hero_thumb/ae93f6696a2a89b67aa6fb45092eded7.jpg",
                LogoUrl = "https://cdn2.steamgriddb.com/logo_thumb/90915208c601cc8c86ad01250ee90c12.png"
            },
            new Game()
            {
                Name = "DOOM",
                PriceUSD = 40,
                Description = "Fight like hell",
                ReleaseYear = 2016,
                PosterUrl = "https://cdn2.steamgriddb.com/thumb/775974bd62116bc3d3b2c51b04192f0c.png",
                BackgroundUrl = "https://shared.steamstatic.com/store_item_assets/steam/apps/379720/library_hero_2x.jpg?t=1573231983",
                LogoUrl = "https://shared.steamstatic.com/store_item_assets/steam/apps/379720/logo_2x.png?t=1573231983"
            },
            new Game()
            {
                Name = "Undertale",
                PriceUSD = 20,
                Description = "UNDERTALE! The RPG game where you don't have to destroy anyone. ",
                ReleaseYear = 2015,
                PosterUrl = "https://cdn2.steamgriddb.com/thumb/14ec86d482ff9638392a061bfa431a1a.jpg",
                BackgroundUrl = "https://shared.steamstatic.com/store_item_assets/steam/apps/391540/library_hero.jpg?t=1579095961",
                LogoUrl = "https://shared.steamstatic.com/store_item_assets/steam/apps/391540/logo.png?t=1579095961"
            },
            new Game()
            {
                Name = "DELTARUNE",
                PriceUSD = 25,
                Description = "DELTARUNE! The RPG game where your choices don't matter. ",
                ReleaseYear = 2025,
                PosterUrl = "https://deltarune.com/assets/images/key-art.gif",
                BackgroundUrl = "https://deltarune.com/assets/images/bg.gif",
                LogoUrl = "https://deltarune.com/assets/images/logo.png"
            }, 
            new Game()
            {
                Name = "Grand Theft Auto V",
                PriceUSD = 40,
                Description = "Grand Theft Auto V for PC offers players the option to explore the award-winning world of Los Santos and Blaine County in resolutions of up to 4k and beyond, as well as the chance to experience the game running at 60 frames per second.",
                ReleaseYear = 2013,
                PosterUrl = "https://cdn2.steamgriddb.com/thumb/86f045465e82c214dc5e68ba530546ba.jpg",
                BackgroundUrl = "https://images.steamusercontent.com/ugc/11669731338331342254/D8FF2435AC1815F69543C8DEE34D15D52399A3DA/?imw=2048&imh=1152&ima=fit&impolicy=Letterbox&imcolor=%23000000&letterbox=true",
                LogoUrl = "https://cdn2.steamgriddb.com/logo_thumb/e5b294b70c9647dcf804d7baa1903918.png"
            }, 
            new Game()
            {
                Name = "Red Dead Redemption 2",
                PriceUSD = 40,
                Description = "Winner of over 175 Game of the Year Awards and recipient of over 250 perfect scores, RDR2 is the epic tale of outlaw Arthur Morgan and the infamous Van der Linde gang, on the run across America at the dawn of the modern age. ",
                ReleaseYear = 2018,
                PosterUrl = "https://cdn2.steamgriddb.com/thumb/e746c3c588c51ad5efcc7125e3df662c.jpg",
                BackgroundUrl = "https://cdn2.steamgriddb.com/hero_thumb/81e5f81db77c596492e6f1a5a792ed53.jpg",
                LogoUrl = "https://shared.steamstatic.com/store_item_assets/steam/apps/1174180/logo_2x.png?t=1671484934"
            }, 
            new Game()
            {
                Name = "Terraria",
                PriceUSD = 5,
                Description = "Also try Minecraft!",
                ReleaseYear = 2011,
                PosterUrl = "https://images-ext-1.discordapp.net/external/ftJWBEe_E9ZCBdz6EEqxSXK4b5r_9zFTEimtI9KII7Q/https/cdn2.steamgriddb.com/thumb/301c8008a981254f98950cebef344b58.jpg?format=webp",
                BackgroundUrl = "https://shared.steamstatic.com/store_item_assets/steam/apps/105600/library_hero.jpg?t=1666290502",
                LogoUrl = "https://shared.steamstatic.com/store_item_assets/steam/apps/105600/logo_2x.png?t=1666290502"
            },
            new Game()
            {
                Name = "Counter-Strike",
                Description = "Play the world's number 1 online action game. Engage in an incredibly realistic brand of terrorist warfare in this wildly popular team-based game. Ally with teammates to complete strategic missions. Take out enemy sites. ",
                PriceUSD = 5,
                ReleaseYear = 2000,
                PosterUrl = "https://cdn2.steamgriddb.com/thumb/6bf8cff2494ff41052ac8474df638cdb.jpg",
                BackgroundUrl = "https://cdn2.steamgriddb.com/hero_thumb/1be3614ec5d67a9fe3fd389516f369ea.jpg",
                LogoUrl = "https://cdn2.steamgriddb.com/logo_thumb/13d429db192fbc7b5cabf9b936cf78e1.png"
            },
            new Game() 
            { 
                Name = "Team Fortress 2",
                Description = "After 9 years in development.",
                PriceUSD = 0,
                ReleaseYear = 2009,
                PosterUrl = "https://cdn2.steamgriddb.com/thumb/2eaa17f7324d93370a43a7b8d55d038e.jpg",
                BackgroundUrl = "https://shared.steamstatic.com/store_item_assets/steam/apps/440/library_hero.jpg?t=1745368576",
                LogoUrl = "https://shared.steamstatic.com/store_item_assets/steam/apps/440/logo.png?t=1745368576"
            });
    }
    public DbSet<Game> Games { get; set; }
    public DbSet<Review> Reviews { get; set; }
    // Unlimited games, but no games
}
