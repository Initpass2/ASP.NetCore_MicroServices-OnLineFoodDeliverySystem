using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMF.Common.Commands;
using OMF.Common.Commands.User;
using RawRabbit;
using System.Threading.Tasks;

namespace OMF.Api.Controllers
{
    [Route("api/[controller]")]   
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IBusClient _busClient;
        public UsersController(IBusClient busClient)
        {
            _busClient = busClient;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody]CreateUser command)
        {
            await _busClient.PublishAsync(command);
            return Accepted($"resgister/{command.Name}");
        }

        [HttpPut("update")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Put([FromBody]UpdateUser command)
        {
            await _busClient.PublishAsync(command);
            return Accepted($"update/{command.UpdatedEmail}");
        }

        [HttpDelete("unregister")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete([FromBody]DeleteUser command)
        {
            await _busClient.PublishAsync(command);
            return Accepted($"unregister/{command.Email}");
        }


    }
}