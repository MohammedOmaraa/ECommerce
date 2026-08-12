using ECommerce.Application.Contracts;
using ECommerce.Application.DTO_s.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{

    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto, CancellationToken ct = default)
        {
            var result = await authenticationService.LoginAsync(loginDto, ct);
            return ToActionResult(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto, CancellationToken ct = default)
        {
            var result = await authenticationService.RegisterAsync(registerDto, ct);
            return ToActionResult(result);
        }
    }
}
