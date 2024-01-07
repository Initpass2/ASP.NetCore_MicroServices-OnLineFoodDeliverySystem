using OMF.Common.Events;
using OMF.Common.Events.Restaurant;
using OMF.RestaurantManagement.Services;
using System.Threading.Tasks;

namespace OMF.RestaurantManagement.Handler
{
    public class OrderUpdatedHandler : IEventHandler<OrderUpdated>
    {
        private readonly IRestaurantService _service;
        public OrderUpdatedHandler(IRestaurantService restaurantService)
        {
            _service = restaurantService;
        }

        public async Task HandleAsync(OrderUpdated @event)
        {

            int diff = 0;
            int finalCount = 0;
            foreach (var item in @event.MenuItemStocks)
            {
                int cnt = await _service.GetMenuItemCount(@event.RestaurantId, item.MenuId);
                if (item.NewItemCount > item.OldItemCount)
                {
                    diff = item.NewItemCount - item.OldItemCount;
                    finalCount = cnt - diff;
                }
                else if (item.OldItemCount > item.NewItemCount)
                {
                    diff = item.OldItemCount - item.NewItemCount;
                    finalCount = cnt + diff;
                }
                else
                    finalCount = cnt;

                await _service.UpdateMenuItemCount(@event.RestaurantId, item.MenuId, cnt + finalCount);
            }

        }
    }
}
