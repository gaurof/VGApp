using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using VGAppDb.Models;

namespace VGAppDb.Repositories
{
    public class ReviewsRepository : IReviewsRepository
    {
        private readonly VGAppDbContext _context;

        public ReviewsRepository(VGAppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Game>> GetReviewsByGameName(string name)
        {
            return await _context.Games
                .Where(g => g.Name == name)
                .Include(g => g.Reviews)
                .ThenInclude(g => g.User)
                .ToListAsync();
        }

        public async Task<Review?> GetReviewByIdAsync(int id)
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Game)
                .Include(r => r.UsersThatLiked)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddReviewAsync(Review review)
        {
            ArgumentNullException.ThrowIfNull(review);

            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(Review review) =>
            await GetReviewByIdAsync(review.Id) is not null;

        public async Task ToggleLikeAsync(string userId, int reviewId)
        {
            var review = await _context.Reviews
                .Include(r => r.UsersThatLiked)
                .FirstOrDefaultAsync(r => r.Id == reviewId) ??
                throw new ArgumentException("Review not found");

            var user = await _context.Users.FindAsync(userId) ??
                throw new ArgumentException("User not found");

            var existingLike = review.UsersThatLiked
                .FirstOrDefault(u => u.Id == userId);

            if (existingLike is null)
                review.UsersThatLiked.Add(user);
            else review.UsersThatLiked.Remove(user);

            await _context.SaveChangesAsync();
        }

        public async Task<int> GetLikeCountAsync(int reviewId)
        {
            return await _context.Reviews
                .Where(r => r.Id == reviewId)
                .SelectMany(r => r.UsersThatLiked)
                .CountAsync();
        }
    }
}