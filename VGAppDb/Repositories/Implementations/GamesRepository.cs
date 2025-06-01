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
    public async Task<List<Game>> GetGames(int? amount = null)
    {
        var query = _context.Games.AsQueryable();

        if (amount.HasValue)
            query = query.Take(amount.Value);

        return await query.ToListAsync() ?? [];
    }
    public async Task<Game?> GetGameByIdAsync(int id)
    {
        return await _context.Games
            .Include(g => g.Reviews)
            .FirstOrDefaultAsync(g => g.Id == id);
    }
    public async Task<Game?> GetGameByPropertiesAsync(Game game)
    {
        return await _context.Games
                .Include(g => g.Reviews)
                .FirstOrDefaultAsync(g => game.Equals(g));
    }
    public async Task<bool> ExistsByIdAsync(int id) =>
        await GetGameByIdAsync(id) is not null;
    public async Task<bool> ExistsByPropertiesAsync(Game game) =>
        await _context.Games.AnyAsync(g => g.Equals(game));

    public async Task AddGameAsync(Game game)
    {
        if (await ExistsByPropertiesAsync(game))
            throw new InvalidOperationException("Game with these properties already exists");
        await _context.Games.AddAsync(game);
        await _context.SaveChangesAsync();
    }
    public async Task AddGamesAsync(IEnumerable<Game> games)
    {
        foreach (Game game in games)
            await AddGameAsync(game);
    }

    public async Task UpsertGameAsync(Game game)
    {
        var existing = await GetGameByPropertiesAsync(game);

        if (existing is not null)
        {
            game.Id = existing.Id;
            await EditGameAsync(game.Id, game);
        }
        else
            await AddGameAsync(game);
        await _context.SaveChangesAsync();
    }
    public async Task UpsertGamesAsync(IEnumerable<Game> games)
    {
        foreach (var game in games)
            await UpsertGameAsync(game);
    }

    public async Task EditGameAsync(int id, Game gameNew)
    {
        if (!await ExistsByIdAsync(id))
            throw new ArgumentException($"Game with ID {id} not found");

        var game = await GetGameByIdAsync(id);

        _context.Entry(game!).CurrentValues.SetValues(gameNew);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteGameAsync(int id)
    {
        var game = await _context.Games.FindAsync(id);
        if (game is not null)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }
    }

}