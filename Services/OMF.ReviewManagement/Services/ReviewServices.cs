using OMF.Common.Exception;
using OMF.ReviewManagement.Domain;
using OMF.ReviewManagement.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace OMF.ReviewManagement.Services
{
    public class ReviewServices : IReviewServices
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewServices(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task AddReviewAsync(Review review)
        {
            review.UpdatedOn = DateTime.Now;
            await _reviewRepository.AddAsync(review);
        }

        public async Task DeleteReviewAsynce(Guid orderId)
        {
            var reviewData = await GetReviewAsync(orderId);
            if (reviewData == null)
                throw new OMFException("", "No review found");
            await _reviewRepository.DeleteAsync(reviewData);
        }


        public async Task<Review> GetReviewAsync(Guid orderId)
        {
            return await _reviewRepository.GetReviewAsync(orderId);
        }

        public async Task UpdateReviewAsync(Review review)
        {
            var _review = await _reviewRepository.GetReviewAsync(review.OrderId);
            if(_review == null)
                throw new OMFException("", "No review found");

            _review.ReviewText = review.ReviewText;
            _review.IsManualReviewRequired = review.IsManualReviewRequired;
            _review.ModeratorTrackingId = review.ModeratorTrackingId;
            _review.Rating = review.Rating;
            _review.UpdatedOn = DateTime.Now;

           await _reviewRepository.UpdateAsync(_review);
        }
    }
}
