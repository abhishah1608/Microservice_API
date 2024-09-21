using api.Domain.Entities;
using api.Domain.Interfaces;

namespace api.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> AddUser(User user)
        {
            return _userRepository.AddUser(user).Result;
        }

        public async Task<User> deleteUser(string username, string password)
        {
            return _userRepository.deleteUser(username, password).Result;
        }

        public Task<User> GetUserByNameAndPassword(string username, string password)
        {
            return _userRepository.GetUserByNameAndPassword(username, password);
        }

        public Task<User> UpdateUser(User user)
        {
            return _userRepository.UpdateUser(user);
        }
    }
}
