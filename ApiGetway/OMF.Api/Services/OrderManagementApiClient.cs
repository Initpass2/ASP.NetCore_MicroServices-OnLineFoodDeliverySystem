using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OMF.Api.RequestModel.Order;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OMF.Api.Services
{
    public class OrderManagementApiClient : ControllerBase,IOrderManagementApiClient
    {
        private readonly HttpClient _apiClient;
        private readonly ILogger<OrderManagementApiClient> _logger;
        private readonly UrlsConfig _urls;

        public OrderManagementApiClient(HttpClient httpClient, ILogger<OrderManagementApiClient> logger, IOptions<UrlsConfig> options)
        {
            _apiClient = httpClient;
            _logger = logger;
            _urls = options.Value;
        }     

        public async Task<IActionResult> CreateOrder(OrderManagemenDataModel order)
        {
            try
            {
                var uri = _urls.Order + UrlsConfig.OderOpertions.CreateOrder();
                var response = await _apiClient.PostAsJsonAsync(uri, order);

                response.EnsureSuccessStatusCode();

                var menulists = await response.Content.ReadAsStringAsync();

                return Ok("Order Created Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
           
        }

        public async Task<IActionResult> UpdateOrder(OrderManagemenDataModel order)
        {
            try
            {
                var uri = _urls.Order + UrlsConfig.OderOpertions.UpdateOrder();
                var response = await _apiClient.PutAsJsonAsync(uri, order);

                response.EnsureSuccessStatusCode();

                var menulists = await response.Content.ReadAsStringAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
          
        }

        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            try
            {
                var uri = _urls.Order + UrlsConfig.OderOpertions.CancelOrder(orderId);
                var response = await _apiClient.DeleteAsync(uri);

                response.EnsureSuccessStatusCode();

                var menulists = await response.Content.ReadAsStringAsync();

                return Ok("Order deleted Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            try
            {
                var uri = _urls.Order + UrlsConfig.OderOpertions.GetOrder(orderId);
                var response = await _apiClient.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                var reponse = await response.Content.ReadAsStringAsync();

                return Ok(JsonConvert.DeserializeObject<OrderManagemenDataModel>(reponse));
              
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
