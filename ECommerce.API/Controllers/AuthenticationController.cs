using ECommerce.Application.Contracts;
using ECommerce.Application.DTO_s.Identity;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("email-exists")]
        public async Task<ActionResult<bool>> EmailExists([FromQuery] string email, CancellationToken ct = default)
        {
            var result = await authenticationService.CheckEmailAsync(email, ct);
            return ToActionResult(result);
        }

        [HttpGet("current-user")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser([FromQuery] string email, CancellationToken ct = default)
        {
            var result = await authenticationService.GetCurrentUserAsync(email, ct);
            return ToActionResult(result);
        }

        [HttpGet("address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> GetUserAddress([FromQuery] string email, CancellationToken ct = default)
        {
            var result = await authenticationService.GetUserAddressAsync(email, ct);
            return ToActionResult(result);
        }

        [HttpPost("address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress([FromBody] AddressDto addressDto, [FromQuery] string email, CancellationToken ct = default)
        {
            var result = await authenticationService.UpdateUserAddressAsync(addressDto, email, ct);
            return ToActionResult(result);
        }

    }
}
