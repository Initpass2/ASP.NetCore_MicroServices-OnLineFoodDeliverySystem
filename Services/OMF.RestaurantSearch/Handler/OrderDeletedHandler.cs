using OMF.Common.Events;
using OMF.Common.Events.Restaurant;
using OMF.RestaurantManagement.Services;
using System;
using System.Threading.Tasks;

namespace OMF.RestaurantManagement.Handler
{
    public class OrderDeletedHandler : IEventHandler<OrderDeleted>
    {
        private readonly IRestaurantService _service;
        public OrderDeletedHandler(IRestaurantService restaurantService)
        {
            _service = restaurantService;
        }

        public async Task HandleAsync(OrderDeleted @event)
        {
            int restautrentId = @event.RestaurantId;
            foreach(var item in @event.menuItemStocks)
            {
                int cnt = await _service.GetMenuItemCount(restautrentId, item.MenuId);              
                await _service.UpdateMenuItemCount(@event.RestaurantId,item.MenuId, cnt + item.OldItemCount);
            }
            
        }
    }
}
