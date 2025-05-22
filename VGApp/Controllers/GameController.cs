using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using VGAppDb;
using VGAppDb.Models;
using VGAppDb.Repositories;

namespace VGApp.Controllers;

public class GameController : Controller
{
    private readonly IGamesRepository _repository;
    public GameController(IGamesRepository repository)
    {
        _repository = repository;
    }
    [HttpGet("game/{id}")]
    public async Task<IActionResult> Id(Guid id)
    {
        var game = await _repository.GetGameByIdAsync(id);
        if (game is null)
            return NotFound();
        return View(game);
    }

    [HttpPost]
    public async Task<IActionResult> AddReview(Guid gameId, double rating, string text)
    {
        var game = await _repository.GetGameByIdAsync(gameId);
        if (game is null)
            return NotFound();

        var review = new Review
        {
            GameId = gameId,
            Rating = rating,
            Text = text,
            UserName = User.Identity.Name, // Or your user system
            CreatedDate = DateTime.UtcNow
        };

        game.Reviews.Add(review);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", new { id = gameId });
    }
}
