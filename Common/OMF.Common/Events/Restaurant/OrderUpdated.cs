using System.Collections.Generic;

namespace OMF.Common.Events.Restaurant
{
    public class OrderUpdated : IEvent
    {
        public int RestaurantId { get; set; }
        public List<MenuItemStock> MenuItemStocks { get; set; }

        public OrderUpdated(int restaurantId, List<MenuItemStock> menuItemStocks)
        {
            RestaurantId = restaurantId;
            MenuItemStocks = menuItemStocks;
        }

        protected OrderUpdated()
        {

        }
    }
}
