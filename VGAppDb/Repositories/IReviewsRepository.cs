using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGAppDb.Models;

namespace VGAppDb.Repositories;

public interface IReviewsRepository
{
    Task<List<Game>> GetReviewsByGameId(int id);
    Task<Game?> GetReviewByIdAsync(int id);

    Task AddReviewAsync(Review review);
    Task DeleteReviewAsync(int id);
}
