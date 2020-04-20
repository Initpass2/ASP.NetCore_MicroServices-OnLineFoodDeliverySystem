using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using OMF.Common.Commands;
using OMF.Common.Events;
using OMF.Common.RabbitMq;
using RawRabbit;
using System;

namespace OMF.Common.Services
{
    public class ServiceHost : IServiceHost
    {
        private readonly IWebHost _webHost;

        public ServiceHost(IWebHost webHost)
        {
            _webHost = webHost;
        }

        public void Run()
        {
            _webHost.Run();
        }

        public static HostBuilder Create<TStartup>(string[] args) where TStartup : class
        {
            Console.Title = typeof(TStartup).Namespace;
            var config = new ConfigurationBuilder()
              .AddEnvironmentVariables()
              .AddCommandLine(args)
              .Build();

            var webHostBuilder = WebHost.CreateDefaultBuilder(args)
              .UseConfiguration(config)
              .UseDefaultServiceProvider(options =>
                  options.ValidateScopes = false)
              .UseStartup<TStartup>();

            return new HostBuilder(webHostBuilder.Build());

        }

    }

    public abstract class BuilderBase
    {
        public abstract ServiceHost Build();
    }

    public class HostBuilder : BuilderBase
    {
        private readonly IWebHost _webHost;
        private IBusClient _bus;

        public HostBuilder(IWebHost webHost)
        {
            _webHost = webHost;
        }

        public BusBuilder UserRabbitMq()
        {
            _bus = (IBusClient)_webHost.Services.GetService(typeof(IBusClient));
            return new BusBuilder(_webHost, _bus);
        }


        public override ServiceHost Build()
        {
            return new ServiceHost(_webHost);
        }
    }

    public class BusBuilder : BuilderBase
    {
        private readonly IWebHost _webHost;
        private IBusClient _bus;

        public BusBuilder(IWebHost webHost, IBusClient bus)
        {
            _webHost = webHost;
            _bus = bus;
        }


        public BusBuilder SubscribeToCommad<TCommand>() where TCommand : ICommand
        {
            try
            {
                var handler = (ICommandHandler<TCommand>)_webHost.Services.GetService(typeof(ICommandHandler<TCommand>));
                _bus.WithCommandHandlerAsync(handler);
                return this;
            }
            catch (System.Exception ex)
            {
                throw;
            }

        }

        public BusBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent
        {
            try
            {
                var handler = (IEventHandler<TEvent>)_webHost.Services
               .GetService(typeof(IEventHandler<TEvent>));
                _bus.WithEventHandlerAsync(handler);

                return this;
            }
            catch (System.Exception ex)
            {

                throw;
            }

        }

        public override ServiceHost Build()
        {
            return new ServiceHost(_webHost);
        }
    }
}
