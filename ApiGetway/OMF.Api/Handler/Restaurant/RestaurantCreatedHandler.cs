using NetTopologySuite;
using OMF.Api.DTO.Repositories;
using OMF.Common.Events;
using OMF.Common.Events.Restaurant;
using System.Threading.Tasks;

namespace OMF.Api.Handler.Restaurant
{
    public class RestaurantCreatedHandler : IEventHandler<RestaurantCreatedOrUpdated>
    {
        private readonly IRestaurantRepository _repository;
        public RestaurantCreatedHandler(IRestaurantRepository repository)
        {
            _repository = repository;
        }
        public async Task HandleAsync(RestaurantCreatedOrUpdated @event)
        {
            await Task.CompletedTask;
          
            await _repository.AddRestaurantAsync(new DTO.Models.Restaurant
            {
                Address = @event.Address,
                Budget = @event.Budget,
                Cuisine = @event.Cuisine,
                IsActive = @event.IsActive,
                Location = @event.Location,
                Name = @event.Name,
                Rating = @event.Rating,
                MasterRestaurantID = @event.RestaurantID
            });
        }
    }
}