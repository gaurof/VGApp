using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VGAppDb.Models;
using VGAppDb.Repositories;

namespace VGApp.Controllers
{
    [Authorize(Roles = Constants.AdminRoleName)]
    public class AdminController(IGamesRepository gamesRepository) : Controller
    {
        private readonly IGamesRepository gamesRepository = gamesRepository;

        // GET: Admin/Edit/Index

        public async Task<IActionResult> Index()
        {
            return View(await gamesRepository.GetGames());
        }

        // GET: Admin/Edit/Create
        public IActionResult CreateGame()
        {
            return View();
        }

        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,ReleaseYear,PosterUrl,BackgroundUrl,LogoUrl")] Game game)
        {
            if (ModelState.IsValid)
            {
                game.Name = game.Name.Trim();
                game.Description = (game.Description ?? "").Trim();
                if (await gamesRepository.ExistsAsync(game))
                {
                    ModelState.AddModelError(string.Empty, "A game with this name already exists.");
                    return View(game);
                }
                await gamesRepository.AddGameAsync(game);
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Admin/Edit/gameName
        [Route("Admin/Edit/{gameName}")]
        public async Task<IActionResult> Edit(string? gameName)
        {
            if (string.IsNullOrEmpty(gameName) ||
                !await gamesRepository.ExistsAsync(gameName!))
                return NotFound();

            var game = await gamesRepository.GetGameByNameAsync(gameName!);
            return View(game);
        }

        // POST: Admin/Edit/Edit/gameName
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Name,Description,Price,ReleaseYear,PosterUrl,BackgroundUrl,LogoUrl")] Game game)
        {
            if (!await gamesRepository.ExistsAsync(game))
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    await gamesRepository.EditGameAsync(game);
                }
                catch (Exception ex) 
                { 
                    Console.WriteLine(ex); 
                }
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Admin/Delete/gameName
        public async Task<IActionResult> Delete(string? gameName)
        {
            if (string.IsNullOrEmpty(gameName) ||
                !await gamesRepository.ExistsAsync(gameName!))
                return NotFound();
            var game = await gamesRepository.GetGameByNameAsync(gameName!);

            return View(game);
        }

        // POST: Admin/Delete/gameName
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await gamesRepository.DeleteGameAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
