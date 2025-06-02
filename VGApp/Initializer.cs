using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using VGAppDb;
using VGAppDb.Models;
using VGAppDb.Repositories;
using static System.Net.WebRequestMethods;

namespace VGApp;

public class Initializer()
{

    public static async Task InitializeIdentity(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        var adminUsername = "admin";
        var adminPassword = "123Qweasd.";

        if (await roleManager.FindByNameAsync(Constants.AdminRoleName) is null)
            await roleManager.CreateAsync(new IdentityRole(Constants.AdminRoleName));

        if (await roleManager.FindByNameAsync(Constants.UserRoleName) is null)
            await roleManager.CreateAsync(new IdentityRole(Constants.UserRoleName));

        if (await userManager.FindByNameAsync(adminUsername) is null)
        {
            var adminUser = new User { UserName = adminUsername };
            var creationResult = await userManager.CreateAsync(adminUser, adminPassword);
            if (creationResult.Succeeded)
                await userManager.AddToRoleAsync(adminUser, Constants.AdminRoleName);
        }
    }

    public static async Task CreatePlaceholderGames(IGamesRepository gamesRepository)
    {
        Game[] games =
        [
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
            }
        ];
        await gamesRepository.UpsertGamesAsync(games);
    }
}
