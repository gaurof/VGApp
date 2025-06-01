using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VGAppDb.Models;

namespace VGAppDb.Repositories;

public interface IGamesRepository
{
    Task<List<Game>> GetGames();
    Task<List<Game>> GetGames(int amount);
    Task<Game?> GetGameByIdAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<bool> ExistsAsync(Game game);

    Task AddGameAsync(Game game);
    Task EditGameAsync(int id, Game game);
    Task DeleteGameAsync(int id);
}