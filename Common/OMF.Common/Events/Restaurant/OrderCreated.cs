using System.Collections.Generic;

namespace OMF.Common.Events.Restaurant
{
    public class OrderCreated : IEvent
    {
        public int RestaurantId { get; set; }
        public List<MenuItemStock> MenuItemStocks { get; set; }

        public OrderCreated(int restaurantId, List<MenuItemStock> menuItemStocks)
        {
            RestaurantId = restaurantId;
            MenuItemStocks = menuItemStocks;
        }

        protected OrderCreated()
        {
        }
    }
}
