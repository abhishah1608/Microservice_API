using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Domain.Entities
{
    public class User
    {
        [SwaggerSchema(ReadOnly = true)] // This property will not appear in Swagger
        public int UserId { get; set; }

        [SwaggerSchema(Description = "Username of the user, should be unique for every user.")]
        public string? UserName { get; set; }

        [SwaggerSchema(Description = "Firstname of the user.")]
        public string? FirstName { get; set; }

        [SwaggerSchema(Description = "Lastname of the user.")]
        public string? LastName { get; set; }

        [SwaggerSchema(Description = "Email Address of the user.")]
        public string? Email { get; set; }

        [SwaggerSchema(Description = "Password of the user.")]
        public string? PasswordHash { get; set; }
        public string? Role { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public DateTime CreatedAt { get; set; }

        [SwaggerSchema(Description = "Is User Active, 0 - Inactive and 1- Active")]
        public int IsActive { get; set; }


        [SwaggerSchema(ReadOnly = true)] // This property will not appear in Swagger
        public string? Token { get; set; }
    }
}
