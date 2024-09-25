using api.Application.Services;
using api.Domain.Entities;
using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [AllowAnonymous]
        [HttpPost]
        [Route("addNewUser")]
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
                ModelState.AddModelError("AddNewUser", ex.Message.ToString());
                string msg = ex.Message;
                return BadRequest(msg);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin user)
        {
            var result = default(User);
            try
            {
                result = await _userService.GetUserByNameAndPassword(user.userName, user.password);  // Await the result properly
                result.PasswordHash = null;
                return Ok(result);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Login", ex.Message.ToString());
                string msg = ex.Message;
                return Unauthorized(msg);
            }
        }

        [HttpGet]
        [Route("access")]
        public async Task<IActionResult> Test()
        {
            return Ok("Authorized Access");
        }

    }
}
