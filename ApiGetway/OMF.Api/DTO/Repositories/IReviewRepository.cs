using OMF.Api.DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMF.Api.DTO.Repositories
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetReviewsAsync();
        Task<Review> GetReviewAsync(Guid orderId);
        Task<List<Review>> GetReviewsAsync(int restaurantId);
        Task CreateReview(Review review);
        Task UpdateReview(Review review);
        Task DeleteReview(Guid reviewId);
    }
}
