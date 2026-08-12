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

        public async Task<Result<bool>> CheckEmailAsync(string email, CancellationToken ct = default)
        {
            return await identityService.EmailExistsAsync(email, ct);
        }

        public async Task<Result<UserDto>> GetCurrentUserAsync(string email, CancellationToken ct = default)
        {
            var userResult = await identityService.FindByEmailAsync(email, ct);

            if (!userResult.IsSuccess)
            {
                return Result<UserDto>.Failure(userResult.Errors);
            }

            var user = userResult.Value;

            var rolesResult = await identityService.GetRolesAsync(user.Email, ct);

            if (!rolesResult.IsSuccess)
            {
                return Result<UserDto>.Failure(rolesResult.Errors);
            }

            var token = tokenService.CreateToken(user.Id, user.Email, user.UserName, rolesResult.Value);

            return Result<UserDto>.Success(new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = token
            });
        }

        public async Task<Result<AddressDto>> GetUserAddressAsync(string email, CancellationToken ct = default)
        {
            var result = await identityService.GetAddressByEmailAsync(email, ct);
            if (!result.IsSuccess)
            {
                return Result<AddressDto>.Failure(result.Errors);
            }

            return Result<AddressDto>.Success(result.Value);
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

        public async Task<Result<AddressDto>> UpdateUserAddressAsync(AddressDto addressDto, string email, CancellationToken ct = default)
        {
            return await identityService.UpdateAddressByEmailAsync(email, addressDto, ct);
        }
    }
}
