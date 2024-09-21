using api.Domain.Entities;
using api.Domain.Interfaces;
using System.Text;
using UtilityProj;
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
                sb.Append("IF Exists(SELECT 1 FROM Users WITH(NOLOCK) WHERE Email = @Email) ");
                sb.AppendLine();
                sb.Append("BEGIN ");
                sb.AppendLine();
                sb.Append("    RAISERROR('Email Address is already used, please enter another Email Address', 16, 1); ");
                sb.AppendLine();
                sb.Append("END ");
                sb.AppendLine();
                sb.Append("ELSE IF Exists(SELECT 1 FROM Users WITH(NOLOCK) WHERE UserName = @UserName) ");
                sb.AppendLine();
                sb.Append("BEGIN ");
                sb.AppendLine();
                sb.Append("    RAISERROR('UserName is already used, please enter another username', 16, 1); ");
                sb.AppendLine();
                sb.Append("END ");
                sb.AppendLine();
                sb.Append("ELSE ");
                sb.AppendLine();
                sb.Append("BEGIN ");
                sb.AppendLine();
                sb.Append("    INSERT INTO Users ");
                sb.AppendLine();
                sb.Append("        (FirstName, LastName, Email, PasswordHash, Role, CreatedAt, IsActive, UserName) ");
                sb.AppendLine();
                sb.Append("    VALUES ");
                sb.AppendLine();
                sb.Append("        (@FirstName, @LastName, @Email, @PasswordHash, @Role, @CreatedAt, @IsActive, @UserName) ");
                sb.AppendLine();
                sb.Append("END ");
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
