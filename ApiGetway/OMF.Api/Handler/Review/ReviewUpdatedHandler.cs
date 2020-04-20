using OMF.Api.DTO.Repositories;
using OMF.Common.Events;
using OMF.Common.Events.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMF.Api.Handler.Review
{
    public class ReviewUpdatedHandler : IEventHandler<ReviewUpdated>
    {
        private readonly IReviewRepository _repository;
        public ReviewUpdatedHandler(IReviewRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(ReviewUpdated @event)
        {          
            await _repository.UpdateReview(new DTO.Models.Review()
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