using OMF.Common.Events;
using OMF.Common.Events.Order;
using OMF.Common.Events.Restaurant;
using OMF.Common.Events.Review;
using OMF.Common.Events.User;
using OMF.Common.Services;

namespace OMF.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
              .UserRabbitMq()
              .SubscribeToEvent<UserCreated>()
              .SubscribeToEvent<UserUpdated>()
              .SubscribeToEvent<UserDeleted>()
              .SubscribeToEvent<ReviewCreated>()
              .SubscribeToEvent<ReviewRejected>()
              .SubscribeToEvent<ReviewUpdated>()
              .SubscribeToEvent<ReviewDeleted>()
              .SubscribeToEvent<OrderAdded>()
              .SubscribeToEvent<OrderModified>()
              .SubscribeToEvent<OrderCanceled>()
              .SubscribeToEvent<RestaurantCreatedOrUpdated>()
              .Build()
              .Run();
        }

        //public static void Main(string[] args)
        //{
        //    CreateWebHostBuilder(args).Build().Run();
        //}

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>();
    }
}
