using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using VGApp.ViewModels;
using VGAppDb;
using VGAppDb.Models;
using VGAppDb.Repositories;

namespace VGApp.Controllers
{
    public class HomeController(IGamesRepository gamesRepository, UserManager<User> userManager) : Controller
    {
        private readonly IGamesRepository _gamesRepository = gamesRepository;
        private readonly UserManager<User> _userManager = userManager;

        public async Task<IActionResult> Index()
        {
            var viewModel = new UserWithGamesViewModel()
            {
                Games = await _gamesRepository.GetGames(20) ?? [],
                User = await _userManager.GetUserAsync(User)
            };
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
