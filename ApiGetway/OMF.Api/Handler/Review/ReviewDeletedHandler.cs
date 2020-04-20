using OMF.Api.DTO.Repositories;
using OMF.Common.Events;
using OMF.Common.Events.Review;
using System.Threading.Tasks;

namespace OMF.Api.Handler.Review
{
    public class ReviewDeletedHandler : IEventHandler<ReviewDeleted>
    {
        private readonly IReviewRepository _repository;
        public ReviewDeletedHandler(IReviewRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(ReviewDeleted @event)
        {
            await _repository.DeleteReview(@event.ReviewId);
        }
    }
}