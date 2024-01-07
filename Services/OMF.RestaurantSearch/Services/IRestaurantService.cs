using OMF.RestaurantSearch.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMF.RestaurantManagement.Services
{
    public interface IRestaurantService
    {
       // Task<IEnumerable<Restaurant>> GetRestaurants(string restaurantName, decimal rating, string cuisine, int budget, double latitude, double longitude, double distance);
        Task<IEnumerable<Menu>> GetMenus(int restaurantId);
        Task CreateRestaurant(Restaurant restaurant);
        Task<int> GetMenuItemCount(int restaurantId, int menuId);
        Task UpdateMenuItemCount(int restaurantId, int menuId,int itemCount);
    }
}
