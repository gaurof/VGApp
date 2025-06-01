using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VGAppDb.Models;

namespace VGAppDb.Repositories;

public interface IGamesRepository
{
    Task<List<Game>> GetGames(int? amount = null);
    Task<Game?> GetGameByIdAsync(int id);
    Task<bool> ExistsByIdAsync(int id);
    Task<bool> ExistsByPropertiesAsync(Game game);

    Task AddGameAsync(Game game);
    Task AddGamesAsync(IEnumerable<Game> games);

    Task UpsertGameAsync(Game game);
    Task UpsertGamesAsync(IEnumerable<Game> game);

    Task EditGameAsync(int id, Game game);
    
    Task DeleteGameAsync(int id);
}