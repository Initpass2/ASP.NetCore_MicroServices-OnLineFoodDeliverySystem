using Microsoft.AspNetCore.Mvc;
using OMF.OrderManagement.Service;
using System;
using System.Threading.Tasks;

namespace OMF.OrderManagement.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManagementService _service;
        public OrderController(IOrderManagementService service)
        {
            _service = service;
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> Post(OrderManagementDataModel order)
        {
            try
            {
                if (order == null) return BadRequest("Request body can't be null");
                await _service.ProcessCreateOrderRequest(order);
                return Ok("Order created successfully");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("UpdateOrder")]
        public async Task<IActionResult> Put(OrderManagementDataModel order)
        {
            try
            {
                if (order == null) return BadRequest("Request body can't be null");
                await _service.ProcessUpdateOrderRequest(order);
                return Ok("Order updated successfully");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex);
            }            

        }

        [HttpDelete("DeleteOrder/{orderid}")]
        public  async Task<IActionResult> Delete(Guid orderid)
        {
            try
            {
                await _service.ProcessCancelOrderRequest(orderid);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("GetOrder/{orderid}")]
        public async Task<IActionResult> Get(Guid orderid)
        {
            try
            {
                return Ok( await _service.ProcessGetOrderDetails(orderid));             
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}