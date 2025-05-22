using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using VGApp.Models;
using VGAppDb;
using VGAppDb.Models;
using VGAppDb.Repositories;

namespace VGApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGamesRepository _gamesRepository;

        public HomeController(IGamesRepository gamesRepository)
        {
            _gamesRepository = gamesRepository;
        }

        public async Task<IActionResult> Index()
        {
            var games = await _gamesRepository.GetGames(8) ?? new List<Game>();
            return View(games);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
