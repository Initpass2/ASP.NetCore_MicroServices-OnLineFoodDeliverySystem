using OMF.Common.Events;
using OMF.Common.Events.User;
using System;
using System.Threading.Tasks;

namespace OMF.Api.Handler.User
{
    public class UserDeletedHandler : IEventHandler<UserDeleted>
    {
        public async Task HandleAsync(UserDeleted @event)
        {
            await Task.CompletedTask;
            Console.WriteLine($"User deleted : {@event.Email}");
        }
    }
}