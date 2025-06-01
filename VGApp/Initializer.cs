using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using VGAppDb;
using VGAppDb.Models;
using VGAppDb.Repositories;

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
        var minecraft = new Game()
        {
            Name = "Minecraft",
            PriceUSD = 20,
            ReleaseYear = 2011,
            PosterUrl = "https://cdn2.steamgriddb.com/thumb/a73027901f88055aaa0fd1a9e25d36c7.jpg",
            BackgroundUrl = "https://cdn2.steamgriddb.com/hero_thumb/ae93f6696a2a89b67aa6fb45092eded7.jpg",
            LogoUrl = "https://cdn2.steamgriddb.com/logo_thumb/3d2c20fb145bba2747e87ecf8321bcfc.webm"
        };

        await gamesRepository.AddGameAsync(minecraft);
    }
}
