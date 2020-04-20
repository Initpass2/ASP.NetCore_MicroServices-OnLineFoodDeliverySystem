using OMF.Common.Events.Restaurant;
using OMF.Common.Services;

namespace OMF.RestaurantSearch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();
            ServiceHost.Create<Startup>(args)
              .UserRabbitMq()
              .SubscribeToEvent<OrderCreated>()
              .SubscribeToEvent<OrderUpdated>()
              .SubscribeToEvent<OrderDeleted>()
              .Build()
              .Run();
        }

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>();
    }
}
