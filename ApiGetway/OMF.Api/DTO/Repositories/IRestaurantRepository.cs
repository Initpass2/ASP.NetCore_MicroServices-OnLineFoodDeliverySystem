using OMF.Api.DTO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMF.Api.DTO.Repositories
{
    public interface IRestaurantRepository
    {        
        Task<List<RestaurantBrowserModel>> GetRestaurantsAsyncByCauisine(string cuisine);
        Task<List<RestaurantBrowserModel>> GetRestaurantsAsyncByName(string name);
        Task<List<RestaurantBrowserModel>> GetRestaurantsAsync(int budget, decimal rating);
        Task<List<RestaurantBrowserModel>> GetRestaurantsAsync(double latitude, double longitude);
        Task AddRestaurantAsync(Restaurant restaurant);
    }
}
