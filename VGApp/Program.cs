using Microsoft.EntityFrameworkCore;
using VGAppDb;
using Microsoft.AspNetCore.Identity;
using VGAppDb.Models;
using Microsoft.Extensions.DependencyInjection;
using VGAppDb.Repositories;

namespace VGApp;

public class Program
{
    public static void Main(string[] args)
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
        builder.Services.AddDbContext<AuthDbContext>(options => 
            options
            .UseMySql(connectionString, serverVersion)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()); 
        builder.Services.AddScoped<IGamesRepository, GamesRepository>();
        builder.Services.AddScoped<IReviewsRepository, ReviewsRepository>();

        builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AuthDbContext>();
        builder.Services.AddRazorPages();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();
        app.MapStaticAssets();


        app.MapControllerRoute(
            name: "Admin",
            pattern: "{area:Exists}/{controller=Home}/{action=Index}/{id?}");

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