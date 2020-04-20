using OMF.Common.Auth;
using OMF.Common.Commands.User;
using OMF.Common.Exception;
using OMF.CustomerManagement.Domain.Models;
using OMF.CustomerManagement.Domain.Repositories;
using OMF.CustomerManagement.Domain.Services;
using System.Threading.Tasks;

namespace OMF.CustomerManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IEncrypter _encrypter;
        private readonly IJwtHandler _jwtHandler;

        public UserService(IUserRepository userRepository, IEncrypter encrypter, IJwtHandler jwtHandler)
        {
            _repository = userRepository;
            _encrypter = encrypter;
            _jwtHandler = jwtHandler;
        }

        public async Task<JsonWebToken> LoginAsync(string email, string password)
        {
            var user = await UserDbValidation(email);

            if (!user.ValidatePassword(password, _encrypter))
            {
                throw new OMFException("invalid credentials", $"Invalid credential");
            }

            return _jwtHandler.Create(user.Id);
        }

        public async Task RegisterAsync(string email, string password, string name)
        {
            var user = await _repository.GetAsync(email);
            if (user != null)
                throw new OMFException("", "User already exists");
            user = new User(email, name);
            user.SetPassword(password, _encrypter);
            await _repository.AddAsync(user);
        }

        public async Task UnRegisterAsync(string email)
        {
            var user = await UserDbValidation(email);
            await _repository.DeleteAsync(user);
        }

        public async Task UpdateAsync(UpdateUser updateUser)
        {
            var user =await UserDbValidation(updateUser.OldEmail);

            user.Email = updateUser.UpdatedEmail;
            user.Name = updateUser.Name;

            await _repository.UpdateAsync(user);
        }

        private async Task<User> UserDbValidation(string email)
        {
            var user = await _repository.GetAsync(email);
            if (user == null)
            {
                throw new OMFException("No user found", $"No user found");
            }
            return user;
        }
    }
}
