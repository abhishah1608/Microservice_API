using api.Application.Services;
using api.Domain.Entities;
using API.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Template.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ServiceFilter(typeof(ValidationFilter))]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="externalServiceCall"></param>
        public UserController(UserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        [Route("AddNewUser")]
        public async Task<IActionResult> AddNewUser([FromBody] User user)
        {
            var result = default(User);
            try
            {
                result = await _userService.AddUser(user);  // Await the result properly
                return Created("User created successfully", user.UserName);// Return the result, not the Task
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return BadRequest(msg);
            }
        }

    }
}
