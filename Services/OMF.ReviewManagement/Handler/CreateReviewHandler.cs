using Microsoft.Extensions.Logging;
using OMF.Common.Commands;
using OMF.Common.Commands.Review;
using OMF.Common.Events.Review;
using OMF.Common.Exception;
using OMF.ReviewManagement.Domain;
using OMF.ReviewManagement.Moderator;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace OMF.ReviewManagement.Handler
{
    public class CreateReviewHandler : ICommandHandler<CreateReview>
    {
        private readonly IBusClient _client;
        private readonly IReviewValidation _reviewValidation;
        private readonly ILogger _logger;

        public CreateReviewHandler(IBusClient busClient, IReviewValidation reviewValidation, ILogger<CreateReview> logger)
        {
            _client = busClient;
            _reviewValidation = reviewValidation;
            _logger = logger;
        }

        public async Task HandleAsync(CreateReview command)
        {
            _logger.LogInformation($"Creating review : {command.TextReview}");

            try
            {
                Guid reviewid = Guid.NewGuid();
                DateTime dateTime = DateTime.Now;
                bool isSucess = await _reviewValidation.ValidateReview(new Review()
                {
                    OrderId = command.OrderId,
                    Rating = command.Rating,
                    ResturentID = command.ResturentID,
                    ResturentName = command.ResturentName,
                    ReviewId = reviewid,
                    ReviewText = command.TextReview,
                    UserName = command.UserName,
                    UpdatedOn = dateTime
                }, "add");

                
                if (isSucess)
                    await _client.PublishAsync(new ReviewCreated(command.TextReview, command.Rating, command.ResturentName, command.ResturentID
                        ,command.UserName, dateTime,command.OrderId, reviewid));
                else
                    await _client.PublishAsync(new ReviewRejected("Review will be posted within an hour ", "400", command.UserName, command.OrderId));

                return;
            }
            catch (OMFException ex)
            {
                await _client.PublishAsync(new ReviewRejected(ex.Message, ex.Code, command.UserName, command.OrderId));

                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                await _client.PublishAsync(new ReviewRejected( "error", ex.Message, command.UserName, command.OrderId));
                _logger.LogError(ex.Message);
            }
        }
    }
}
