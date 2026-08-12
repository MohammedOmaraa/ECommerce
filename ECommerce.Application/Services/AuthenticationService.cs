using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO_s.Identity;
using Microsoft.AspNetCore.Rewrite;
using static ECommerce.Application.Common.ResultOfT;

namespace ECommerce.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IIdentityService identityService;
        private readonly ITokenService tokenService;

        public AuthenticationService(IIdentityService identityService, ITokenService tokenService)
        {
            this.identityService = identityService;
            this.tokenService = tokenService;
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

            var rolesResult = await identityService.GetRolesAsync(userResult.Value.Email, ct);

            if (!rolesResult.IsSuccess)
            {
                return Result<UserDto>.Failure(rolesResult.Errors);
            }

            var token = tokenService.CreateToken(userResult.Value.Id, userResult.Value.Email, userResult.Value.UserName, rolesResult.Value);

            return Result<UserDto>.Success(new UserDto 
            { 
                Email = loginDto.Email,
                DisplayName = userResult.Value.DisplayName,
                Token = token
            });
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
            var userResult = await identityService.CreateUser(registerDto, ct);

            if (!userResult.IsSuccess)
            {
                return Result<UserDto>.Failure(userResult.Errors);
            }

            var rolesResult = await identityService.GetRolesAsync(userResult.Value.Email, ct);

            if (!rolesResult.IsSuccess)
            {
                return Result<UserDto>.Failure(rolesResult.Errors);
            }

            var token = tokenService.CreateToken(userResult.Value.Id, userResult.Value.Email, userResult.Value.UserName, rolesResult.Value);


            return Result<UserDto>.Success(new UserDto
            {
                Email = userResult.Value.Email,
                DisplayName = userResult.Value.DisplayName,
                Token = token
            });
        }
    }
}
