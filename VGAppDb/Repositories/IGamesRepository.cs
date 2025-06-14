using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VGAppDb.Models;

namespace VGAppDb.Repositories;

public interface IGamesRepository
{
    Task<List<Game>> GetGames(int? amount = null);
    Task<Game?> GetGameByNameAsync(string name);
    Task<bool> ExistsAsync(Game game);
    Task<bool> ExistsAsync(string name);

    Task AddGameAsync(Game game);
    Task AddGamesAsync(IEnumerable<Game> games);

    Task UpsertGameAsync(Game game);
    Task UpsertGamesAsync(IEnumerable<Game> game);

    Task EditGameAsync(Game game);
    
    Task DeleteGameAsync(string name);
    Task DeleteGameAsync(Game game);
    Task TogglePlayed(Game game, User user);
}