using Microsoft.Extensions.Logging;
using OMF.Common.Commands;
using OMF.Common.Events;
using OMF.Common.Exception;
using OMF.CustomerManagement.Services;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace OMF.CustomerManagement.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IBusClient _client;
        private readonly IUserService _service;
        private readonly ILogger _logger;

        public CreateUserHandler(IBusClient busClient, IUserService userService,
          ILogger<CreateUserHandler> logger)
        {
            _client = busClient;
            _service = userService;
            _logger = logger;
        }

        public async Task HandleAsync(CreateUser command)
        {
            _logger.LogInformation($"Creating user : {command.Email} {command.Name}");

            try
            {
                await _service.RegisterAsync(command.Email, command.Password, command.Name);
                await _client.PublishAsync(new UserCreated(command.Email, command.Name));
                return;
            }
            catch (OMFException ex)
            {
                await _client.PublishAsync(new UserRejected(command.Email,
                  ex.Code, ex.Message));
                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                await _client.PublishAsync(new UserRejected(command.Email,
                  "error", ex.Message));
                _logger.LogError(ex.Message);
            }
        }
    }
}
