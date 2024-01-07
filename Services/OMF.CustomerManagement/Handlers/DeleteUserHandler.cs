using Microsoft.Extensions.Logging;
using OMF.Common.Commands;
using OMF.Common.Commands.User;
using OMF.Common.Events;
using OMF.Common.Events.User;
using OMF.Common.Exception;
using OMF.CustomerManagement.Services;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace OMF.CustomerManagement.Handlers
{
    public class DeleteUserHandler : ICommandHandler<DeleteUser>
    {
        private readonly IBusClient _client;
        private readonly IUserService _service;
        private readonly ILogger _logger;

        public DeleteUserHandler(IBusClient busClient, IUserService userService, ILogger<CreateUserHandler> logger)
        {
            _client = busClient;
            _service = userService;
            _logger = logger;
        }

        public async Task HandleAsync(DeleteUser command)
        {
            _logger.LogInformation($"Deleting user : {command.Email}");

            try
            {
                await _service.UnRegisterAsync(command.Email);
                await _client.PublishAsync(new UserDeleted(command.Email));
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
