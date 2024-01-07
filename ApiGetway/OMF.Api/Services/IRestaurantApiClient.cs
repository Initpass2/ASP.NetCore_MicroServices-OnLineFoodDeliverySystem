using OMF.Api.ResponseModel.Restaurant;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMF.Api.Services
{
    public interface IRestaurantApiClient
    {
        Task<IEnumerable<Menu>> GetRestaurentMenuAsync(int restaurentId);
    }
}
