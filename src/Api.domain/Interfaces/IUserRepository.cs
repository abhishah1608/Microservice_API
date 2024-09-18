using api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Domain.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// called during login, it authenticate the user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task<User> GetUserByNameAndPassword(string username, string password);

        /// <summary>
        /// save new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<User> AddUser(User user);

        /// <summary>
        /// Used to update the user object.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<User> UpdateUser(User user);

        /// <summary>
        /// It will inactivate the user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task<User> deleteUser(string username, string password);

    }
}
