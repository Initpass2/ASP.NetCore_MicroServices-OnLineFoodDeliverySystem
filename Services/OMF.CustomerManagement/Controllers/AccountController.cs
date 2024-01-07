using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OMF.Common.Commands;
using OMF.CustomerManagement.Services;
using System.Threading.Tasks;

namespace OMF.CustomerManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]AuthenticateUser authenticateUser)
        {
            try
            {
                return Content(JsonConvert.SerializeObject(await _userService.LoginAsync(authenticateUser.Email, authenticateUser.Password)));
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
          
        }
       

    }
}