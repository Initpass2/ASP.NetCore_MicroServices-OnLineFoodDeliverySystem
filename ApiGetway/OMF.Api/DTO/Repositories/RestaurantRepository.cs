using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using OMF.Api.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMF.Api.DTO.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        // suvanakr : 9/15/2019
        // For simplicity I am creating different method for restaurent seach
        // We can write sql dynmic query for restaurant search 

        private readonly OMFStorageContext _context;

        public RestaurantRepository(OMFStorageContext context)
        {
            _context = context;
        }

        public async Task<List<RestaurantBrowserModel>> GetRestaurantsAsyncByCauisine(string cuisine)
        => await _context.restaurants
            .Where(r => r.Cuisine.Contains(cuisine, StringComparison.InvariantCultureIgnoreCase))
            .Select(x => new RestaurantBrowserModel()
            {
                Coordinates = x.Location.Coordinates.Select(c => new { X = c.X, Y = c.Y, Z = c.Z }),
                Address = x.Address,
                Budget = x.Budget,
                Cuisine = x.Cuisine,
                IsActive = x.IsActive,
                MasterRestaurantID = x.MasterRestaurantID,
                Name = x.Name,
                Rating = x.Rating
            })
            .ToListAsync();

        //ToListAsync<Restaurant>();

        public async Task<List<RestaurantBrowserModel>> GetRestaurantsAsyncByName(string name)
           => await _context.restaurants
            .Where(r => r.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase))
            .Select(x => new RestaurantBrowserModel()
            {
                Coordinates = x.Location.Coordinates.Select(c => new { X = c.X, Y = c.Y, Z = c.Z }),
                Address = x.Address,
                Budget = x.Budget,
                Cuisine = x.Cuisine,
                IsActive = x.IsActive,
                MasterRestaurantID = x.MasterRestaurantID,
                Name = x.Name,
                Rating = x.Rating
            })
            .ToListAsync();

        public async Task<List<RestaurantBrowserModel>> GetRestaurantsAsync(int budget, decimal rating)
               => await _context.restaurants
            .Where(r => r.Budget == budget && r.Rating >= rating).
            Select(x => new RestaurantBrowserModel()
            {
                Coordinates = x.Location.Coordinates.Select(c => new { X = c.X, Y = c.Y, Z = c.Z }),
                Address = x.Address,
                Budget = x.Budget,
                Cuisine = x.Cuisine,
                IsActive = x.IsActive,
                MasterRestaurantID = x.MasterRestaurantID,
                Name = x.Name,
                Rating = x.Rating
            })
            .ToListAsync();

        public async Task<List<RestaurantBrowserModel>> GetRestaurantsAsync(double latitude, double longitude)
        {
            // Convert the input latitude and longitude to a Point  
            var location = new Point(latitude, longitude) { SRID = 4326 };

            // Fetch the tourist attractions and their  
            // distances from the input location   
            // using spatial queries.  
            var result = await _context.restaurants.Select(t => new
            {
                Restaurant = t,
                Distance = t.Location.Distance(location)
            }).ToListAsync();

            // Ordering the result in the ascending order of distance  

            var restaurants = result.OrderBy(x => x.Distance).Select(x => new RestaurantBrowserModel()
            {
                Coordinates = x.Restaurant.Location.Coordinates.Select(c => new { X = c.X, Y = c.Y, Z = c.Z }),
                Address = x.Restaurant.Address,
                Budget = x.Restaurant.Budget,
                Cuisine = x.Restaurant.Cuisine,
                IsActive = x.Restaurant.IsActive,
                MasterRestaurantID = x.Restaurant.MasterRestaurantID,
                Name = x.Restaurant.Name,
                Rating = x.Restaurant.Rating
            }).ToList();

            return restaurants;

            //var result = restaurant
            //    .OrderBy(x => x.Distance)
            //    .Select(t => new
            //    {
            //        Distance = Math.Round(t.Distance, 6),
            //        Latitude = t.Place.Location.X,
            //        Longitude = t.Place.Location.Y,
            //        Placename = t.Place.Name
            //    }).ToList();


        }

        // When a new restaurant will be set up add restaurant in here as well for quick search
        public async Task AddRestaurantAsync(Restaurant restaurant)
        {
            _context.Add(restaurant);
            await _context.SaveChangesAsync();
        }

    }
}
