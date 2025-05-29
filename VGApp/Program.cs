using Microsoft.EntityFrameworkCore;
using VGAppDb;
using Microsoft.AspNetCore.Identity;
using VGAppDb.Models;
using Microsoft.Extensions.DependencyInjection;
using VGAppDb.Repositories;
using VGApp.Models;

namespace VGApp;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 40));

        builder.Services.AddDbContext<VGAppDbContext>(options => 
            options
            .UseMySql(connectionString, serverVersion)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors());
        builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<VGAppDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddScoped<IGamesRepository, GamesRepository>();
        builder.Services.AddScoped<IReviewsRepository, ReviewsRepository>();

        builder.Services.AddRazorPages();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromDays(2);
            options.LoginPath = "/User/Account/Login";
            options.LogoutPath = "/User/Account/Logout";
            options.Cookie = new CookieBuilder() { IsEssential = true };
        });
        await using var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await IdentityInitializer.Initialize(userManager, roleManager);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding identity data.");
            }
        }
        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapStaticAssets();


        app.MapControllerRoute(
            name: Constants.AdminRoleName,
            pattern: "{area:Exists}/{controller=Edit}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: Constants.UserRoleName,
            pattern: "{area:Exists}/{controller=Account}/{action=Login}/{id?}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.MapRazorPages();

        app.Run();
    }
}

// Добавить пользователей с регистрацией, входом, ролями и именами в отзывах
// Лайки на отзывы
// Сортировка отзывов по дате, популярности
// Комментарии к отзывам с лайками
// Страница пользователя с его отзывами и списком сыграных игр
// Поиск игр