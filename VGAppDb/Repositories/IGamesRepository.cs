using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VGAppDb.Models;

namespace VGAppDb.Repositories;

public interface IGamesRepository
{
    Task<IEnumerable<Game>> GetRecentGamesAsync(int count);
    Task<Game?> GetGameByIdAsync(Guid id);

    Task AddGameAsync(Game game);
    Task UpdateGameAsync(Game game);
    Task DeleteGameAsync(Guid id);

    Task<bool> SaveChangesAsync();
}