using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using OMF.Common.Services;

namespace OMF.OrderManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //ServiceHost.Create<Startup>(args)
            // .UserRabbitMq()
            // .Build()
            // .Run();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
