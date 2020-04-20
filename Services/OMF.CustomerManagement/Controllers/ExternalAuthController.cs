using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OMF.Common.Auth;
using OMF.CustomerManagement.Auth;
using OMF.CustomerManagement.Domain.Repositories;
using OMF.CustomerManagement.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OMF.CustomerManagement.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ExternalAuthController : ControllerBase
    {
        private static readonly HttpClient Client = new HttpClient();
        private readonly IUserRepository _userRepository;
        private readonly FacebookAuthSettings _fbAuthSettings;
        private readonly IJwtHandler _jwtHandler;
        private readonly IUserService _userService;
        public ExternalAuthController(IUserRepository userRepository, IUserService userService,
            IOptions<FacebookAuthSettings> fbAuthSettingsAccessor, IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _fbAuthSettings = fbAuthSettingsAccessor.Value;
            _jwtHandler = jwtHandler;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Facebook([FromBody]FacebookAuthModel model)
        {
            try
            {
                // 1.generate an app access token
                var appAccessTokenResponse = await Client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_fbAuthSettings.AppId}&client_secret={_fbAuthSettings.AppSecret}&grant_type=client_credentials");
                var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);
                // 2. validate the user access token
                var userAccessTokenValidationResponse = await Client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={model.AccessToken}&access_token={appAccessToken.AccessToken}");
                var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

                if (!userAccessTokenValidation.Data.IsValid)
                {
                    return BadRequest($"login_failure . Invalid facebook token.");
                }

                // we've got a valid token so we can request user data from fb
                var userInfoResponse = await Client.GetStringAsync($"https://graph.facebook.com/me?fields=id,name,email,first_name,last_name,gender,picture&access_token={model.AccessToken}");

                var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

                // suvanakar 
                // I cann't get password here but code is successfully able to retrieve other propert from FB
                // In this assignmet not mentioned whether I have to store this info or not for futher refenence.
                // So, for now I just adding userid into local db just to know how many users used this application

                // 4. ready to create the local user account (if necessary) and jwt
                if (await _userRepository.GetAsync(userInfo.Email) == null)
                {
                    await _userService.RegisterAsync(userInfo.Email, userInfo.Email, userInfo.Name);
                }

                var jwt = _jwtHandler.Create(Guid.NewGuid());

                return new OkObjectResult(jwt);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
