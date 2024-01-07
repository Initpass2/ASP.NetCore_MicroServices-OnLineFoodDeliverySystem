using OMF.ReviewManagement.Domain;
using System;
using System.Threading.Tasks;

namespace OMF.ReviewManagement.Services
{
    public interface IReviewServices
    {
        Task<Review> GetReviewAsync(Guid orderId);
        Task AddReviewAsync(Review review);
        Task UpdateReviewAsync(Review review);
        Task DeleteReviewAsynce(Guid orderId);        
    }
}
