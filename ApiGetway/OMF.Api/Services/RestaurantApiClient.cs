using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OMF.Api.ResponseModel.Restaurant;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace OMF.Api.Services
{
    public class RestaurantApiClient : IRestaurantApiClient
    {
        private readonly HttpClient _apiClient;
        private readonly ILogger<RestaurantApiClient> _logger;
        private readonly UrlsConfig _urls;

        public RestaurantApiClient(HttpClient httpClient, ILogger<RestaurantApiClient> logger, IOptions<UrlsConfig> options)
        {
            _apiClient = httpClient;
            _logger = logger;
            _urls = options.Value;
        }

        public async Task<IEnumerable<Menu>> GetRestaurentMenuAsync(int restaurentId)
        {
            var uri = _urls.Restaurant + UrlsConfig.RestaurantOperations.GetMenuByRestaurantId(restaurentId);
            var response = await _apiClient.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            var menulists = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<Menu>>(menulists);
        }
    }
}
