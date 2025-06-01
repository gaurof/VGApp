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
                Id = 64,
                Name = "Minecraft",
                Description = "Also try terraria!",
                PriceUSD = 20,
                ReleaseYear = 2011,
                PosterUrl = "https://cdn2.steamgriddb.com/thumb/a73027901f88055aaa0fd1a9e25d36c7.jpg",
                BackgroundUrl = "https://cdn2.steamgriddb.com/hero_thumb/ae93f6696a2a89b67aa6fb45092eded7.jpg",
                LogoUrl = "https://cdn2.steamgriddb.com/logo_thumb/3d2c20fb145bba2747e87ecf8321bcfc.webm"
            },
            new Game()
            {
                Id = 666,
                Name = "DOOM",
                PriceUSD = 40,
                Description = "Fight like hell",
                ReleaseYear = 2016,
                PosterUrl = "https://cdn2.steamgriddb.com/thumb/e6b2e5d385c1503fbd55b97ba5dc4b77.jpg",
                BackgroundUrl = "https://cdn2.steamgriddb.com/hero_thumb/5a2cb441c18f6535a9fb765251345d0f.jpg",
                LogoUrl = "https://cdn2.steamgriddb.com/logo_thumb/ada67ce42f7e51433fdc45e523f90ff7.png"
            },
            new Game()
            {
                Id = 2,
                Name = "Undertale",
                PriceUSD = 20,
                Description = "UNDERTALE! The RPG game where you don't have to destroy anyone. ",
                ReleaseYear = 2015,
                PosterUrl = "https://cdn2.steamgriddb.com/thumb/aba1e5182d973f43f2b3fd755f8e2314.jpg",
                BackgroundUrl = "https://cdn2.steamgriddb.com/hero_thumb/b65f2ecd2900ba6ae49a14d9c4b16fb4.jpg",
                LogoUrl = "https://cdn2.steamgriddb.com/logo_thumb/3a2c1c2b2f19c0d47bf74b4c40cc1753.png"
            }
        ];
        await gamesRepository.UpsertGamesAsync(games);
    }
}
