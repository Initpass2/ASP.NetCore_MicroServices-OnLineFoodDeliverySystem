using OMF.Common.Events;
using System;
using System.Threading.Tasks;

namespace OMF.Api.Handler
{
    public class UserCreatedHandler : IEventHandler<UserCreated>
    {
        public async Task HandleAsync(UserCreated @event)
        {
            await Task.CompletedTask;
            Console.WriteLine($"User created : {@event.Email}");
        }
    }
}
