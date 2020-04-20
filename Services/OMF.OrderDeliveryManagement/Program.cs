using OMF.Common.Events.Delivery;
using OMF.Common.Services;

namespace OMF.OrderDeliveryManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
             .UserRabbitMq()
             .SubscribeToEvent<NofifiedOrderUpdated>()             
             .Build()
             .Run();

            //CreateWebHostBuilder(args).Build().Run();
        }

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>();
    }
}
