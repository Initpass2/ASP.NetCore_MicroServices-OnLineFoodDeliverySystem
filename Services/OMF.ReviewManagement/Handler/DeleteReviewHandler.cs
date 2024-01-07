using Microsoft.Extensions.Logging;
using OMF.Common.Commands;
using OMF.Common.Commands.Review;
using OMF.Common.Events.Review;
using OMF.Common.Exception;
using OMF.ReviewManagement.Services;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace OMF.ReviewManagement.Handler
{
    public class DeleteReviewHandler : ICommandHandler<DeleteReview>
    {
        private readonly IBusClient _client;
        private readonly IReviewServices _review;
        private readonly ILogger _logger;

        public DeleteReviewHandler(IBusClient busClient, IReviewServices reviewServices, ILogger<ReviewDeleted> logger)
        {
            _client = busClient;
            _review = reviewServices;
            _logger = logger;
        }

        public async Task HandleAsync(DeleteReview command)
        {
            _logger.LogInformation($"Deleteing review : {command.ReviewId}");

            try
            {
                await _review.DeleteReviewAsynce(command.OrderId);
                await _client.PublishAsync(new ReviewDeleted(command.ReviewId, command.OrderId));
            }
            catch (OMFException ex)
            {
                await _client.PublishAsync(new ReviewRejected(ex.Message, ex.Code, command.OrderId));
                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                await _client.PublishAsync(new ReviewRejected("error", ex.Message, command.OrderId));
                _logger.LogError(ex.Message);
            }
        }
    }
}
