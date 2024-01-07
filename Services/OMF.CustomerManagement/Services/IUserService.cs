using OMF.Common.Auth;
using OMF.Common.Commands.User;
using System.Threading.Tasks;

namespace OMF.CustomerManagement.Services
{
    public interface IUserService
    {
        Task RegisterAsync(string email, string password, string name);
        Task<JsonWebToken> LoginAsync(string email, string password);
        Task UpdateAsync(UpdateUser updateUser);
        Task UnRegisterAsync(string email);
    }
}
