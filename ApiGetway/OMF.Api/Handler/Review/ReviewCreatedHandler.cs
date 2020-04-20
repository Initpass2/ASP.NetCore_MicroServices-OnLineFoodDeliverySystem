using OMF.Api.DTO.Repositories;
using OMF.Common.Events;
using OMF.Common.Events.Review;
using System.Threading.Tasks;

namespace OMF.Api.Handler.Review
{
    public class ReviewCreatedHandler : IEventHandler<ReviewCreated>
    {
        private readonly IReviewRepository _repository;
        public ReviewCreatedHandler(IReviewRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(ReviewCreated @event)
        {
            await _repository.CreateReview(new DTO.Models.Review()
            {
                MasterReviewId = @event.ReviewId,
                OrderId = @event.OrderId,
                Rating = @event.Rating,
                RestaurantId = @event.ResturentId,
                ReviewText = @event.TextReview,
                UpdatedOn = @event.UpdatedOn,
                UserName = @event.UserName
            });
        }
    }
}