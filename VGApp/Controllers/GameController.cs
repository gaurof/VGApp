using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using VGAppDb;
using VGAppDb.Models;
using VGAppDb.Repositories;

namespace VGApp.Controllers;

public class GameController : Controller
{
    private readonly IReviewsRepository _reviewsRepository;
    private readonly IGamesRepository _gamesRepository; // Assuming you have this

    public GameController(
        IReviewsRepository reviewsRepository,
        IGamesRepository gamesRepository)
    {
        _reviewsRepository = reviewsRepository;
        _gamesRepository = gamesRepository;
    }

    [HttpPost]
    public async Task<IActionResult> AddReview(Guid gameId, float rating, string? text)
    {
        try
        {
            if (rating < 0.5f || rating > 5f)
            {
                ModelState.AddModelError("rating", "Rating must be between 0.5 and 5 stars");
            }
            var game = await _gamesRepository.GetGameByIdAsync(gameId);
            if (!ModelState.IsValid)
            {
                return RedirectToAction(gameId.ToString());
            }
            if (game is null)
                return NotFound();

            var review = new Review
            {
                Id = Guid.NewGuid(),
                Rating = rating,
                Text = text,
                Game = game
            };

            await _reviewsRepository.AddReviewAsync(review);

            return RedirectToAction(gameId.ToString());
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "An error occurred while adding your review";
            return RedirectToAction(gameId.ToString());
        }
    }
    [HttpGet("game/{id}")]
    public async Task<IActionResult> Id(Guid id)
    {
        var game = await _gamesRepository.GetGameByIdAsync(id);
        if (game is null)
            return NotFound();
        return View(game);
    }
}
