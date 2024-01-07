using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMF.Api.RequestModel.Order;
using OMF.Api.Services;
using System;
using System.Net;
using System.Threading.Tasks;

namespace OMF.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManagementApiClient _client;

        public OrderController(IOrderManagementApiClient client)
        {
            _client = client;
        }

        [HttpPost("CreateOrder")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateOrder([FromBody]OrderManagemenDataModel order)
        {
            if (order == null)
                return BadRequest("Order model can't be null");
           return await _client.CreateOrder(order);
        }

        [HttpPut("UpdateOrder")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateOrder([FromBody]OrderManagemenDataModel order)
        {
            if (order == null)
                return BadRequest("Order model can't be null");
            return await _client.UpdateOrder(order);
        }

        [HttpDelete("CancelOrder/{orderid}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> CancelOrder(Guid orderId)
        {
            if (orderId == null) return BadRequest("OrderID can't be null");
            return await _client.DeleteOrder(orderId);
        }


        // suvanakar: 9/25/2019
        // We can retrieve data from OMF.RestaurantManagementStroage db directly that's the reason we have this db 
        // Same as retriving review data. 

        [HttpGet("GetOrder/{orderid}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(OrderManagemenDataModel),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            if (orderId == null) return BadRequest("OrderID can't be null");
            return await _client.GetOrder(orderId);
        }
    }
}