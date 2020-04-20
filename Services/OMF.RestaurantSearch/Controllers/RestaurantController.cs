using Microsoft.AspNetCore.Mvc;
using OMF.RestaurantManagement.Services;
using OMF.RestaurantSearch.Domain.Models;
using RawRabbit;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace OMF.RestaurantManagement.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _service;
        private readonly IBusClient _client;

        public RestaurantController(IRestaurantService service, IBusClient busClient)
        {
            _service = service;
            _client = busClient;
        }

        // suvankar : 9/17/2019
        // This is created for testting 
        [HttpPost("create")]
        public async Task<IActionResult> Post(Entity restaurant)
        {
            // sampple to insert restaurent

            Restaurant obj = new Restaurant();
            obj.Address = restaurant.Address;
            obj.BudgetId = restaurant.BudgetId;
            obj.City = restaurant.City;
            obj.CuisineId = restaurant.CuisineId;
            obj.IsActive = true;
            obj.Name = restaurant.Name;
            obj.Phone = restaurant.Phone;
            obj.Zip = restaurant.Zip;

           await _service.CreateRestaurant(obj);
            
            return Ok();
        }      


        [HttpGet("GetMenuByRestaurantId/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Menu>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMenuByRestaurantId(int id)
        {
            var menulst = await _service.GetMenus(id);
            return Ok(menulst);
        }
    }

    public class Entity
    {    
        public string Name { get; set; }     
        public string Address { get; set; }       
        public string Phone { get; set; }
        public string City { get; set; }       
        public string Zip { get; set; }      
        public int BudgetId { get; set; }     
        public int CuisineId { get; set; }       
        public bool IsActive { get; set; }      
    }
}