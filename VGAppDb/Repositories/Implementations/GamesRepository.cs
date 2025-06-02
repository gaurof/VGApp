using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VGAppDb.Models;

namespace VGAppDb.Repositories;

public class GamesRepository(VGAppDbContext context) : IGamesRepository
{
    private readonly VGAppDbContext _context = context;

    public async Task<List<Game>> GetGames(int? amount = null)
    {
        var query = _context.Games.AsQueryable();

        if (amount.HasValue)
            query = query.Take(amount.Value);

        return await query.ToListAsync() ?? [];
    }
    public async Task<Game?> GetGameByNameAsync(string name)
    {
        return await _context.Games
                .Include(g => g.Reviews)
                .FirstOrDefaultAsync(g => name == g.Name);
    }

    public async Task<bool> ExistsAsync(Game game) =>
        await _context.Games.AnyAsync(g => g.Equals(game));
    public async Task<bool> ExistsAsync(string name) =>
        await _context.Games.AnyAsync(g => g.Name == name);

    public async Task AddGameAsync(Game game)
    {
        ArgumentNullException.ThrowIfNull(game);

        if (await ExistsAsync(game))
            throw new InvalidOperationException("Game with this name already exists");

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
        var existing = await GetGameByNameAsync(game.Name);

        if (existing is not null)
        {
            await EditGameAsync(game);
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

    public async Task EditGameAsync(Game gameNew)
    {
        if (!await ExistsAsync(gameNew))
            throw new ArgumentException($"Game {gameNew.Name} not found");

        var game = await GetGameByNameAsync(gameNew.Name);

        _context.Entry(game!).CurrentValues.SetValues(gameNew);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteGameAsync(string name)
    {
        var game = await GetGameByNameAsync(name);
        if (game is not null)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }
        Console.WriteLine("Tried deleting a game that doesn't exist");
    }
    public async Task DeleteGameAsync(Game game) => await DeleteGameAsync(game.Name);

}