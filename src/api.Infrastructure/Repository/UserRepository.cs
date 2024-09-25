using api.Domain.Entities;
using api.Domain.Interfaces;
using System.Text;
using UtilityProj;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace api.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlConnection _dbconnection = null;
        
        private readonly IConfiguration _configuration = null;

        public UserRepository(SqlConnection dbConnection, IConfiguration configuration)
        {
            _dbconnection = dbConnection;
            _configuration = configuration;
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
            StringBuilder sb = new StringBuilder();
            using (DapperService.DapperService dbservice = new DapperService.DapperService(_dbconnection))
            {
                sb.Append(@"SELECT * FROM USERS WITH(NOLOCK) WHERE USERNAME= @username AND IsActive = 1");
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@username", username);
                User user = await dbservice.QuerySingleOrDefaultAsync<User>(sb.ToString(), parameters);
                if(user != null)
                {
                    string salt = (!string.IsNullOrEmpty(user.FirstName) ? user.FirstName : "") + (!string.IsNullOrEmpty(user.LastName) ? user.LastName : string.Empty);
                    string PasswordHash = HashGenerator.GenerateHashpassword(password, salt);
                    // user is available
                    if(user.PasswordHash == PasswordHash && user.IsActive == 1)
                    {
                        string jwtkey = _configuration["JwtToken:key"].ToString();
                        JwtTokenGenerator jwtTokenGenerator = new JwtTokenGenerator(jwtkey, "Online_Course_Admin", "Online_Course_Users");
                        string token = jwtTokenGenerator.GenerateToken(username, user.Role);
                        user.Token = token;
                        return user;
                    }
                    else
                    {
                        throw new Exception("Please enter the correct password for the user");
                    }

                }
                else
                {
                    throw new Exception("Please enter the correct username");
                }
            }
        }
    }
}
