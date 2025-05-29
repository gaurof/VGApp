using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using VGAppDb.Models;

namespace VGApp.Models;

public class IdentityInitializer
{
    public static async Task Initialize(UserManager<User> userManager, RoleManager<IdentityRole> roleManager) 
    {
        var adminUsername = "admin";
        var adminPassword = "123Qweasd.";

        if ((await roleManager.FindByNameAsync(Constants.AdminRoleName)) is null)
            await roleManager.CreateAsync(new IdentityRole(Constants.AdminRoleName));

        if ((await roleManager.FindByNameAsync(Constants.UserRoleName)) is null)
            await roleManager.CreateAsync(new IdentityRole(Constants.UserRoleName));

        if ((await userManager.FindByNameAsync(adminUsername)) is null)
        {
            var adminUser = new User { UserName = adminUsername };
            var creationResult = await userManager.CreateAsync(adminUser, adminPassword);
            if (creationResult.Succeeded) 
                await userManager.AddToRoleAsync(adminUser, Constants.AdminRoleName);
        }
    }
}
