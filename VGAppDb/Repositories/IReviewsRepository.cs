using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGAppDb.Models;

namespace VGAppDb.Repositories;

public interface IReviewsRepository
{
    Task<List<Game>> GetReviewsByGameId(Guid id);
    Task<Game?> GetReviewByIdAsync(Guid id);

    Task AddReviewAsync(Review review);
    Task DeleteReviewAsync(Guid id);
}
