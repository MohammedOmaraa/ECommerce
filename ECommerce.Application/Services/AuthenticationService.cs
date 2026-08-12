
using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO_s.Identity;
using static ECommerce.Application.Common.ResultOfT;

namespace ECommerce.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IIdentityService identityService;

        public AuthenticationService(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default)
        {
            var userResult = await identityService.FindByEmailAsync(loginDto.Email, ct);

            if (!userResult.IsSuccess)
            {
                return Result<UserDto>.Failure(userResult.Errors);
            }

            var passwordCheck = await identityService.CheckPasswordAsync(loginDto.Email, loginDto.Password, ct);

            if (!passwordCheck.IsSuccess || !passwordCheck.Value)
            {
                return Result<UserDto>.Failure(Error.Unauthorized("Invalid email or password."));
            }

            return Result<UserDto>.Success(new UserDto 
            { 
                Email = loginDto.Email,
                DisplayName = userResult.Value.DisplayName,
                Token = "Token"
            });
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
            var userResult = await identityService.CreateUser(registerDto, ct);

            if (!userResult.IsSuccess)
            {
                return Result<UserDto>.Failure(userResult.Errors);
            }

            return Result<UserDto>.Success(new UserDto
            {
                Email = userResult.Value.Email,
                DisplayName = userResult.Value.DisplayName,
                Token = "Token"
            });
        }
    }
}
