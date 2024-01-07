using OMF.Common.Events;
using OMF.Common.Events.Restaurant;
using OMF.RestaurantManagement.Services;
using System.Threading.Tasks;

namespace OMF.RestaurantManagement.Handler
{
    public class OrderCreatedHandler : IEventHandler<OrderCreated>
    {
        private readonly IRestaurantService _service;
        public OrderCreatedHandler(IRestaurantService restaurantService)
        {
            _service = restaurantService;
        }

        public async Task HandleAsync(OrderCreated @event)
        {
            // suvanakr 
            // We can user transaction to make sure while updating item count it manage proeprly for irder place 
            // FOr this assegemnt I just keep somple here 
            
            foreach(var item in @event.MenuItemStocks)
            {
                int cnt = await _service.GetMenuItemCount(@event.RestaurantId, item.NewItemCount);
                await _service.UpdateMenuItemCount(@event.RestaurantId,  item.MenuId, cnt - item.NewItemCount);
            }                      
        }
    }
}
