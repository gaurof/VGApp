using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Security.Claims;
using VGAppDb;
using VGAppDb.Models;
using VGAppDb.Repositories;

namespace VGApp.Controllers;

public class GameController(
    IReviewsRepository reviewsRepository,
    IGamesRepository gamesRepository,
    UserManager<User> userManager) : Controller
{
    private readonly IReviewsRepository _reviewsRepository = reviewsRepository;
    private readonly IGamesRepository _gamesRepository = gamesRepository;
    private readonly UserManager<User> _userManager = userManager;

    [Authorize]
    public async Task<IActionResult> AddReview(string gameName, float rating, string? text)
    {
        try
        {
            if (rating < 0.5f || rating > 5f)
                ModelState.AddModelError("rating", "Rating must be between 0.5 and 5 stars");

            var game = await _gamesRepository.GetGameByNameAsync(gameName);
            if (!await _gamesRepository.ExistsAsync(game!))
                return NotFound();
            if (!ModelState.IsValid)
                return RedirectToAction(gameName);

            var review = new Review
            {
                User = (await _userManager.GetUserAsync(User))!,
                Rating = rating,
                Text = text,
                Game = game!
            };

            await _reviewsRepository.AddReviewAsync(review);

            return Redirect($"{nameof(Info)}/{gameName}");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "An error occurred while adding your review";
            return RedirectToAction(gameName);
        }
    }

    [HttpGet]
    [Route("Game/Info/{gameName}")]
    public async Task<IActionResult> Info(string gameName)
    {
        if (!await _gamesRepository.ExistsAsync(gameName))
            return NotFound();

        var game = await _gamesRepository.GetGameByNameAsync(gameName);
        return View(game);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleLike(string gameName, int reviewId)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser is null) return Unauthorized();

        var review = await _reviewsRepository.GetReviewByIdAsync(reviewId);
        if (review is null) return NotFound();

        await _reviewsRepository.ToggleLikeAsync(review.User.Id, reviewId);

        return RedirectToAction("Info", new { gameName });
    }
}