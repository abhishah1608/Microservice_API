using api.Domain.Entities;
using api.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.DapperService;
using UtilityProj;
using System.Data;
using System.Data.SqlClient;

namespace api.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlConnection _dbconnection = null;
           

        public UserRepository(SqlConnection dbConnection)
        {
            _dbconnection = dbConnection;
        }

        /// <summary>
        /// Add users.
        /// </summary>
        /// <param name="user">user</param>
        /// <returns>returns user</returns>
        public async Task<User> AddUser(User user)
        {
            using (DapperService.DapperService dbservice = new DapperService.DapperService(_dbconnection))
            {
                StringBuilder sb = new StringBuilder();
                string salt = (!string.IsNullOrEmpty(user.FirstName) ? user.FirstName : "") + (!string.IsNullOrEmpty(user.LastName) ? user.LastName : string.Empty);
                user.PasswordHash = HashGenerator.GenerateHashpassword(user.PasswordHash, salt);
                user.CreatedAt = DateTime.UtcNow;
                sb.Append(@"INSERT INTO Users 
                    (FirstName, LastName, Email, PasswordHash, Role, CreatedAt, IsActive, UserName)
                    VALUES 
                    (@FirstName, @LastName, @Email, @PasswordHash, @Role, @CreatedAt, @IsActive, @UserName)");

               await dbservice.ExecuteAsync(sb.ToString(), user);
            }
            return user;
        }
   

        public async Task<User> UpdateUser(User user)
        {
            using (DapperService.DapperService dbservice = new DapperService.DapperService(_dbconnection))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"Update Users SET FirstName = @FirstName, PasswordHash= @PasswordHash, 
                    LastName = @LastName, Email = @Email, Role = @Role, IsActive = @IsActive
                    WHERE UserName =  @UserName");

                await dbservice.ExecuteAsync(sb.ToString(), user);
            }
            return user;
        }

        public async Task<User> deleteUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByNameAndPassword(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
