using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OMF.Common.Commands;
using OMF.Common.Commands.User;
using OMF.Common.Events;
using OMF.Common.Events.User;
using OMF.Common.Exception;
using OMF.CustomerManagement.Services;
using RawRabbit;

namespace OMF.CustomerManagement.Handlers
{
    public class UpdateUserHandler : ICommandHandler<UpdateUser>
    {
        private readonly IBusClient _client;
        private readonly IUserService _service;
        private readonly ILogger _logger;

        public UpdateUserHandler(IBusClient busClient, IUserService userService, ILogger<CreateUserHandler> logger)
        {
            _client = busClient;
            _service = userService;
            _logger = logger;
        }

        public async Task HandleAsync(UpdateUser command)
        {
            _logger.LogInformation($"Updating user : {command.UpdatedEmail} {command.Name}");

            try
            {
                await _service.UpdateAsync(command);
                await _client.PublishAsync(new UserUpdated(command.UpdatedEmail, command.Name));
                return;
            }
            catch (OMFException ex)
            {
                await _client.PublishAsync(new UserRejected(command.OldEmail,
                  ex.Code, ex.Message));
                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                await _client.PublishAsync(new UserRejected(command.OldEmail,
                  "error", ex.Message));
                _logger.LogError(ex.Message);
            }
        }
    }
}
