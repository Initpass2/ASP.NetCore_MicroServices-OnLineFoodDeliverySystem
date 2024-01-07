using Microsoft.Extensions.Logging;
using OMF.Common.Commands;
using OMF.Common.Commands.Review;
using OMF.Common.Events.Review;
using OMF.Common.Exception;
using OMF.ReviewManagement.Moderator;
using OMF.ReviewManagement.Services;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace OMF.ReviewManagement.Handler
{
    public class UpdateReviewHandler : ICommandHandler<UpdateReview>
    {
        private readonly IBusClient _client;
        private readonly IReviewValidation _reviewValidation;
        private readonly IReviewServices _review;
        private readonly ILogger _logger;

        public UpdateReviewHandler(IBusClient busClient, IReviewValidation reviewValidation, IReviewServices reviewServices, ILogger<UpdateReview> logger)
        {
            _client = busClient;
            _reviewValidation = reviewValidation;
            _review = reviewServices;
            _logger = logger;
        }

        public async Task HandleAsync(UpdateReview command)
        {
            _logger.LogInformation($"Updating review : {command.TextReview}");

            try
            {
                var review = await _review.GetReviewAsync(command.OrderId);
                if (review != null)
                {
                    review.ReviewText = command.TextReview;
                    review.Rating = command.Rating;
                }

                bool isSucess = await _reviewValidation.ValidateReview(review, "update");

                if (isSucess)
                    await _client.PublishAsync(new ReviewUpdated(review.ReviewText, review.Rating, review.ResturentName, review.ResturentID,
                        review.UserName, review.UpdatedOn, command.OrderId, review.ReviewId));
                else
                    await _client.PublishAsync(new ReviewRejected("Review will be posted within an hour ", "400", review.UserName, command.OrderId));

                return;
            }
            catch (OMFException ex)
            {
                await _client.PublishAsync(new ReviewRejected(ex.Message, ex.Code, "", command.OrderId));

                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                 await _client.PublishAsync(new ReviewRejected("error", ex.Message, "", command.OrderId));
                _logger.LogError(ex.Message);
            }
        }
    }
}
