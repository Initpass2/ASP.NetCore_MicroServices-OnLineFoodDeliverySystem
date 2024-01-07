using System;
using System.Threading.Tasks;

namespace OMF.ReviewManagement.Domain.Repositories
{
    public interface IReviewRepository
    {
        Task<Review> GetAsync(Guid reviewId);
        Task<Review> GetReviewAsync(Guid orderId);
        Task AddAsync(Review review);
        Task UpdateAsync(Review review);
        Task DeleteAsync(Review review);
    }
}
