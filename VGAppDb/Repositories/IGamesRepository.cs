using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VGAppDb.Models;

namespace VGAppDb.Repositories;

public interface IGamesRepository
{
    Task<List<Game>> GetGames();
    Task<List<Game>> GetGames(int amount);
    Task<Game?> GetGameByIdAsync(Guid id);

    Task AddGameAsync(Game game);
    Task DeleteGameAsync(Guid id);
}