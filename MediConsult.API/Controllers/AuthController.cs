using MediConsult.Application.Interfaces;
using MediConsult.Application.UsesCases.Auth.Request;
using MediConsult.Application.UsesCases.Patients.Request;
using MediConsult.Application.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediConsult.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody, Bind] LoginRequest user)
        {
            var validator = new LoginValidator();
            var result = validator.Validate(user);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => string.Format("Property {0} failed validation. Error was: {1}", e.PropertyName, e.ErrorMessage));
                return BadRequest(errors);
            }
            var res = await _authService.Login(user);
            return StatusCode(res.Code, res);
        }

    }
}
