using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMF.Api.DTO.Repositories;
using OMF.Common.Commands.Review;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace OMF.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IBusClient _busClient;
        private readonly IReviewRepository _repo;
        public ReviewController(IBusClient busClient, IReviewRepository reviewRepository)
        {
            _busClient = busClient;
            _repo = reviewRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody]CreateReview createReview)
        {
            await _busClient.PublishAsync(createReview);
            return Accepted($"User review processed for {createReview.UserName}");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Put([FromBody]UpdateReview updateReview)
        {
            await _busClient.PublishAsync(updateReview);
            return Accepted($"User review update received");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody]DeleteReview deleteReview)
        {
            await _busClient.PublishAsync(deleteReview);
            return Accepted($"User review delete received");
        }

      
        [HttpGet("getReview/{orderId}")]      
        public async Task<IActionResult> Get(Guid orderId)
        {
            try
            {
                return Ok(await _repo.GetReviewAsync(orderId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }         
                
        }
    }
}