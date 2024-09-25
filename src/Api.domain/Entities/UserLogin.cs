using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Domain.Entities
{

    public class UserLogin
    {
        [SwaggerSchema(Description = "Username of the user.")]
        public string userName { get; set; }

        [SwaggerSchema(Description = "password of the user.")]
        public string password { get; set; }    
    }
}
