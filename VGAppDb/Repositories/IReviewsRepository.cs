using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGAppDb.Models;

namespace VGAppDb.Repositories;

public interface IReviewsRepository
{
    Task<List<Game>> GetReviewsByGameName(string name);
    Task<Review?> GetReviewByIdAsync(int id);

    Task AddReviewAsync(Review review);
    Task DeleteReviewAsync(int id);

    Task<bool> ExistsAsync(Review review);

    Task ToggleLikeAsync(User user, Review review);
    bool HasUserLikedReview(User user, Review review);
}
