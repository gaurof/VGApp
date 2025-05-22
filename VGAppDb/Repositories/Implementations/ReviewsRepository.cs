using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Game>> GetReviewsByGameId(Guid id)
        {
            return await _context.Games
                .Include(g => g.Reviews)
                .Where(g => g.Id == id)
                .ToListAsync();
        }

        public async Task<Game?> GetReviewByIdAsync(Guid id)
        {
            return await _context.Games
                .Include(g => g.Reviews)
                .FirstOrDefaultAsync(g => g.Reviews.Any(r => r.Id == id));
        }

        public async Task AddReviewAsync(Review review)
        {
            if (review == null)
                throw new ArgumentNullException(nameof(review));

            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(Guid id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }
    }
}