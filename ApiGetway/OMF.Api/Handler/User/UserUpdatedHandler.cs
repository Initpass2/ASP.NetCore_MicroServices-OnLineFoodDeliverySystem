using OMF.Common.Events;
using OMF.Common.Events.User;
using System;
using System.Threading.Tasks;

namespace OMF.Api.Handler.User
{
    public class UserUpdatedHandler : IEventHandler<UserUpdated>
    {
        public async Task HandleAsync(UserUpdated @event)
        {
            await Task.CompletedTask;
            Console.WriteLine($"User updated with : {@event.UpdatedEmail} and {@event.Name}");
        }
    }
}
