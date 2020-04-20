using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace OMF.ReviewManagement.Domain.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly OMFReviewDbContext _context;

        public ReviewRepository(OMFReviewDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Review review)
        {
            _context.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Review review)
        {
            _context.Remove(review);
            await _context.SaveChangesAsync();
        }

        public async Task<Review> GetAsync(Guid reviewId)
        {
            return await _context.reviews.SingleOrDefaultAsync(r => r.ReviewId == reviewId);
        }

        public async Task<Review> GetReviewAsync(Guid orderId)
        {
            return await _context.reviews.SingleOrDefaultAsync(r => r.OrderId == orderId);
        }

        public async Task UpdateAsync(Review review)
        {
            _context.Update(review);
            await _context.SaveChangesAsync();
        }
    }
}
