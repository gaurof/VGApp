using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VGAppDb.Models;

namespace VGAppDb.Repositories;

public class GamesRepository : IGamesRepository
{
    private readonly VGAppDbContext _context;

    public GamesRepository(VGAppDbContext context)
    {
        _context = context;
    }

    public async Task<Game?> GetGameByIdAsync(Guid id)
    {
        return await _context.Games
            .Include(g => g.Reviews)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task AddGameAsync(Game game)
    {
        await _context.Games.AddAsync(game);
    }
    public async Task DeleteGameAsync(Guid id)
    {
        var game = await _context.Games.FindAsync(id);
        if (game is not null)
            _context.Games.Remove(game);
    }
}