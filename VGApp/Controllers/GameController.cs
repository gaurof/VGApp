using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VGApp.ViewModels;
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

    [Route("Game/Info/{gameName}")]
    public async Task<IActionResult> Info(string gameName)
    {
        if (!await _gamesRepository.ExistsAsync(gameName))
            return NotFound();
        var gameWithUserViewModel = new GameWithUserViewModel
        {
            Game = (await _gamesRepository.GetGameByNameAsync(gameName))!,
            User = (await _userManager.GetUserAsync(User))
        };


        return View(gameWithUserViewModel);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TogglePlayed(string gameName)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null) return Unauthorized();

        var game = await _gamesRepository.GetGameByNameAsync(gameName);
        if (game is null) return NotFound();

        await _gamesRepository.TogglePlayed(game, user);

        return RedirectToAction("Info", new { gameName });
    }
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleLike(string gameName, int reviewId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null) return RedirectToAction();

        var review = await _reviewsRepository.GetReviewByIdAsync(reviewId);
        if (review is null) return NotFound();

        await _reviewsRepository.ToggleLikeAsync(user, review);

        return RedirectToAction("Info", new { gameName });
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteReview(string gameName, int reviewId)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser is null) return Unauthorized();

        var review = await _reviewsRepository.GetReviewByIdAsync(reviewId);
        if (review is null) return NotFound();

        await _reviewsRepository.DeleteReviewAsync(review.Id);

        return RedirectToAction("Info", new { gameName });
    }
}