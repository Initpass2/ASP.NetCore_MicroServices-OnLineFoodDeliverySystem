using Microsoft.EntityFrameworkCore;
using OMF.RestaurantSearch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMF.RestaurantManagement.Domain.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly OMFRestaurantManagemetDbContext _dbContext;
        public RestaurantRepository(OMFRestaurantManagemetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateRestaurant(Restaurant restaurant)
        {
            _dbContext.Add(restaurant);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Menu>> GetMenus(int restaurantId)
        {
            List<Menu> menus = new List<Menu>();
            var menuid = await _dbContext.restaurantMenuItems.Where(m => m.ResturantID == restaurantId).Select(m => m.MenuID).ToListAsync();

            foreach (int id in menuid)
            {
                menus.Add(_dbContext.menus.Where(m => m.Id == id).Select(m => new Menu()
                {
                    Id = m.Id,
                    Description = m.Description,
                    Ingredients = m.Ingredients,
                    IsActive = m.IsActive,
                    ItemName = m.ItemName,
                    Price = m.Price
                }).Single());
            }

            return menus;
        }

        public async Task<int> GetMenuItemCount(int restaurantId, int menuId)
        {
            int cnt = await _dbContext.restaurantMenuItems.Where(r => r.ResturantID == restaurantId && r.MenuID == menuId).Select(r => r.ItemCount).SingleOrDefaultAsync();
            return cnt;
        }

        public async Task UpdateMenuItemCount(int restaurantId, int menuId, int itemCount)
        {
            var restaurentMenuItem = await _dbContext.restaurantMenuItems.Where(r => r.ResturantID == restaurantId && r.MenuID == menuId).SingleOrDefaultAsync();
            restaurentMenuItem.ItemCount = itemCount;
            _dbContext.Update(restaurentMenuItem);
            await _dbContext.SaveChangesAsync();
        }
    }
}
