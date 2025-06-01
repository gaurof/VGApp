using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VGAppDb;
using VGAppDb.Models;
using VGAppDb.Repositories;

namespace VGApp.Controllers
{
    [Authorize(Roles = Constants.AdminRoleName)]
    public class AdminController : Controller
    {
        private readonly IGamesRepository gamesRepository;
        public AdminController(IGamesRepository gamesRepository)
        {
            this.gamesRepository = gamesRepository;
        }

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

        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,ReleaseYear,PosterUrl,BackgroundUrl")] Game game)
        {
            if (ModelState.IsValid)
            {
                await gamesRepository.AddGameAsync(game);
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Admin/Edit/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null || !id.HasValue)
                return NotFound();

            if (await gamesRepository.ExistsAsync(id.Value))
                return NotFound();

            var game = await gamesRepository.GetGameByIdAsync(id.Value);
            return View(game);
        }

        // POST: Admin/Edit/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,ReleaseYear,PosterUrl,BackgroundUrl")] Game game)
        {
            if (!await gamesRepository.ExistsAsync(game) || id != game.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    await gamesRepository.EditGameAsync(id, game);
                }
                catch (DbUpdateConcurrencyException ex) 
                { 
                    Console.WriteLine(ex); 
                }
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || !id.HasValue)
                return NotFound();
            if (!await gamesRepository.ExistsAsync(id.Value))
                return NotFound();
            var game = await gamesRepository.GetGameByIdAsync(id.Value);

            return View(game);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await gamesRepository.DeleteGameAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
