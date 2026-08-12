
using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO_s.Identity;
using ECommerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using static ECommerce.Application.Common.ResultOfT;

namespace ECommerce.Application.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken ct = default)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return Result<bool>.Failure(Error.Failure(description: "User not found"));
            }

            var isMatch = await userManager.CheckPasswordAsync(user, password);

            return Result<bool>.Success(isMatch);
        }

        public async Task<Result<IdentityUserResult>> CreateUser(RegisterDto registerDto, CancellationToken ct = default)
        {
            var user = new ApplicationUser
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => Error.Failure(e.Code, e.Description)).ToList();
                return Result<IdentityUserResult>.Failure(errors);
            }

            return Result<IdentityUserResult>.Success(new IdentityUserResult(user.Id, user.Email, user.UserName, user.DisplayName));
        }

        public async Task<Result<IdentityUserResult>> FindByEmailAsync(string email, CancellationToken ct = default)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return Result<IdentityUserResult>.Failure(Error.Failure(description:"User not found"));
            }

            return Result<IdentityUserResult>.Success(new IdentityUserResult(user.Id, user.Email, user.UserName, user.DisplayName));
        }
    }
}
