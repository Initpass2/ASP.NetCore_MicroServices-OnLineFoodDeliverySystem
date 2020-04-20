using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OMF.Api.DTO.Models;

namespace OMF.Api.DTO.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly OMFStorageContext _context;

        public ReviewRepository(OMFStorageContext context)
        {
            _context = context;
        }

        public async Task CreateReview(Review review)
        {
            _context.reviews.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReview(Review review)
        {
            var reviewentity = _context.reviews.Where(x => x.MasterReviewId == review.MasterReviewId).SingleOrDefault();
            reviewentity.Rating = review.Rating;
            reviewentity.ReviewText = review.ReviewText;
            reviewentity.UpdatedOn = review.UpdatedOn;

            _context.reviews.Update(reviewentity);
            await _context.SaveChangesAsync();
        }

        public async Task<Review> GetReviewAsync(Guid orderId)
        {
            return await _context.reviews.Where(o => o.OrderId == orderId).Select(o => o).SingleOrDefaultAsync();
        }

        public async Task<List<Review>> GetReviewsAsync()
        {
            return await _context.reviews.ToListAsync();
        }

        public async Task<List<Review>> GetReviewsAsync(int restaurantId)
        {
            return await _context.reviews.Where(o => o.RestaurantId == restaurantId).ToListAsync();
        }

        public async Task DeleteReview(Guid reviewId)
        {
            var reviewentity = _context.reviews.Where(x => x.MasterReviewId == reviewId).SingleOrDefault();
            _context.reviews.Remove(reviewentity);
            await _context.SaveChangesAsync();
        }
    }
}
