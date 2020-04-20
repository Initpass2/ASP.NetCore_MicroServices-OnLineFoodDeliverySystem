using OMF.Common.Commands;
using OMF.Common.Commands.User;
using OMF.Common.Services;

namespace OMF.CustomerManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
              .UserRabbitMq()
              .SubscribeToCommad<CreateUser>()
              .SubscribeToCommad<UpdateUser>()
              .SubscribeToCommad<DeleteUser>()
              .Build()
              .Run();
        }

    }
}
