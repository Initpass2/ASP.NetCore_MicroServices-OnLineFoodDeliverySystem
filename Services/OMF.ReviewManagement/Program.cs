using OMF.Common.Commands.Review;
using OMF.Common.Services;

namespace OMF.ReviewManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
              .UserRabbitMq()
              .SubscribeToCommad<CreateReview>()
               .SubscribeToCommad<UpdateReview>()
               .SubscribeToCommad<DeleteReview>()
              .Build()
              .Run();
        }
    }
}
