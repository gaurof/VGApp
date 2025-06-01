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
    public async Task<List<Game>> GetGames()
    {
        return await _context.Games
            .ToListAsync() ?? [];
    }
    public async Task<List<Game>> GetGames(int amount)
    {
        return await _context.Games
            .Take(amount)
            .ToListAsync() ?? [];
    }
    public async Task<Game?> GetGameByIdAsync(int id)
    {
        return await _context.Games
            .Include(g => g.Reviews)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<bool> ExistsAsync(int id) => await GetGameByIdAsync(id) is null;
    public async Task<bool> ExistsAsync(Game game) => await ExistsAsync(game.Id);

    public async Task AddGameAsync(Game game)
    {
        await _context.Games.AddAsync(game);
        _context.SaveChanges();
    }
    public async Task EditGameAsync(int id, Game gameNew)
    {
        var game = await GetGameByIdAsync(id);
        game = gameNew;
        _context.SaveChanges();
    }
    public async Task DeleteGameAsync(int id)
    {
        var game = await _context.Games.FindAsync(id);
        if (game is not null)
            _context.Games.Remove(game);
        _context.SaveChanges();
    }

}