using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMF.Api.DTO.Models;
using OMF.Api.DTO.Repositories;
using OMF.Api.ResponseModel.Restaurant;
using OMF.Api.Services;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace OMF.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantRepository _repository;
        private readonly IRestaurantApiClient _client;
        public RestaurantController(IRestaurantRepository restaurantRepository, IRestaurantApiClient restaurantApiClient)
        {
            _repository = restaurantRepository;
            _client = restaurantApiClient;
        }

        [HttpGet("getByName")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<RestaurantBrowserModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByName([FromQuery]string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest($"Restaurant name not provided in query parameter");
            var res = await _repository.GetRestaurantsAsyncByName(name);
            if (res == null)
                return NotFound($"Restaruant with name {name} not found");
            return Ok(res);
        }

        [HttpGet("getByCuisine")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<RestaurantBrowserModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByCuisine([FromQuery]string cuisine)
        {
            if (string.IsNullOrWhiteSpace(cuisine))
                return BadRequest($"Restaurant name not provided in query parameter");
            var res = await _repository.GetRestaurantsAsyncByCauisine(cuisine);
            return Ok(res);
        }


        [HttpGet("getByGeoLocation")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<RestaurantBrowserModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getByGeoLocation([FromQuery]double latitude, double longitude)
        {
            var res = await _repository.GetRestaurantsAsync(latitude, longitude);
            if (res == null)
                return NotFound($"Restaurant not found");
            return Ok(res);
        }


        [HttpGet("getByBudgetAndRating")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<RestaurantBrowserModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> getByBudgetAndRating([FromQuery]int budget, decimal rating)
        {
            var res = await _repository.GetRestaurantsAsync(budget, rating);
            if (res == null)
                return NotFound($"Restaurant not found");
            return Ok(res);
        }


        [HttpGet("getMenu/{restaurantId}")]
        [ProducesResponseType(typeof(IEnumerable<Menu>), (int)HttpStatusCode.OK)]  
        public async Task<IEnumerable<Menu>> getMenu(int restaurantId)
        {
           return await _client.GetRestaurentMenuAsync(restaurantId);
        }
    }
}