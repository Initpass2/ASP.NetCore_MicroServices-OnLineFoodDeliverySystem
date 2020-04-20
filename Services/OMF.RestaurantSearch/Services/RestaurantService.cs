using NetTopologySuite;
using OMF.RestaurantManagement.Domain.Repositories;
using OMF.RestaurantSearch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMF.RestaurantManagement.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _repository;

        public RestaurantService(IRestaurantRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateRestaurant(Restaurant restaurant)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            restaurant.Location = geometryFactory.CreatePoint(new GeoAPI.Geometries.Coordinate(22.175015, 73.042155));
            restaurant.CreatedOn = DateTime.Now;
            restaurant.UpdatedOn = DateTime.Now;
            await _repository.CreateRestaurant(restaurant);
        }

        public async Task<int> GetMenuItemCount(int restaurantId, int menuId)
        {
            return await _repository.GetMenuItemCount(restaurantId, menuId);
        }

        public async Task<IEnumerable<Menu>> GetMenus(int restaurantId)
        {
            try
            {
                return await _repository.GetMenus(restaurantId);
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateMenuItemCount(int restaurantId, int menuId, int itemCount)
        {
            try
            {
                await _repository.UpdateMenuItemCount(restaurantId, menuId, itemCount);
            }
            catch
            {

                throw;
            }
           
        }
    }
}
