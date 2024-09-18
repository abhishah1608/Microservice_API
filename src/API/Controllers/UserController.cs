using api.Application.Services;
using api.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Template.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        private readonly IValidator<User> _validator;


        public UserController(UserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        [Route("AddNewUser")]
        public async Task<IActionResult> AddNewUser([FromBody] User user)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // FluentValidation errors will be included here
            }

            var result = await _userService.AddUser(user);  // Await the result properly
            return Created("User created successfully", user.UserName);  // Return the result, not the Task
        }

    }
}
