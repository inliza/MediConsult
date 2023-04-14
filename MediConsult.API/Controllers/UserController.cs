using MediConsult.Application.Interfaces;
using MediConsult.Application.UsesCases.Auth.Request;
using MediConsult.Application.UsesCases.Patients.Request;
using MediConsult.Application.UsesCases.Users.Request;
using MediConsult.Application.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediConsult.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody, Bind] CreateUserRequest user)
        {
            var validator = new UserCreateValidator();
            var result = validator.Validate(user);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => string.Format("Property {0} failed validation. Error was: {1}", e.PropertyName, e.ErrorMessage));
                return BadRequest(errors);
            }
            var res = await _userService.CreateUser(user);
            return StatusCode(res.Code, res);
        }

    }
}
