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
    [Route("Game/{id}")]
    public async Task<IActionResult> Id(Guid id)
    {
        var game = await _repository.GetGameByIdAsync(id);
        if (game is null)
            return NotFound();
        return View(game);
    }
}
