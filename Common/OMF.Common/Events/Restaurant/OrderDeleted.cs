using System.Collections.Generic;

namespace OMF.Common.Events.Restaurant
{
    public class OrderDeleted : IEvent
    {
        public int RestaurantId { get; set; }
        public List<MenuItemStock> menuItemStocks { get; set; }

        public OrderDeleted(int restaurantId, List<MenuItemStock> list)
        {
            RestaurantId = restaurantId;
            menuItemStocks = list;
        }

        protected OrderDeleted()
        {

        }
    }
}
