using OMF.Common.Events;
using OMF.Common.Events.Review;
using System;
using System.Threading.Tasks;

namespace OMF.Api.Handler.Review
{
    public class ReviewRejectedHandler : IEventHandler<ReviewRejected>
    {
        public async Task HandleAsync(ReviewRejected @event)
        {
            await Task.CompletedTask;
            Console.WriteLine($"Review created : {@event.Reason}");
        }
    }
}