
using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO_s.Identity;
using ECommerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Result<bool>> EmailExistsAsync(string email, CancellationToken ct = default)
        {
            var user = await userManager.FindByEmailAsync(email);
            return Result<bool>.Success(user is not null);
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

        public async Task<Result<AddressDto>> GetAddressByEmailAsync(string email, CancellationToken ct = default)
        {
            var user = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email, ct);

            if (user is null)
            {
                return Result<AddressDto>.Failure(Error.Failure(description: "User not found"));
            }

            if (user.Address is null)
            {
                return Result<AddressDto>.Failure(Error.Failure(description: "Address not found"));
            }

            return Result<AddressDto>.Success(new AddressDto
            {
                Street = user.Address.Street,
                City = user.Address.City,
                Country = user.Address.Country,
                FirstName = user.Address.FirstName,
                LastName = user.Address.LastName
            });
        }

        public async Task<Result<IEnumerable<string>>> GetRolesAsync(string email, CancellationToken ct = default)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return Result<IEnumerable<string>>.Failure(Error.Failure(description: "User not found"));
            }

            var roles = await userManager.GetRolesAsync(user);

            return Result<IEnumerable<string>>.Success(roles);
        }

        public async Task<Result<AddressDto>> UpdateAddressByEmailAsync(string email, AddressDto addressDto, CancellationToken ct = default)
        {
            var user = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email, ct);

            if (user is null)
            {
                return Result<AddressDto>.Failure(Error.Failure(description: "User not found"));
            }

            if (user.Address is null)
            {
                user.Address = new Address()
                {
                    Street = addressDto.Street,
                    City = addressDto.City,
                    Country = addressDto.Country,
                    FirstName = addressDto.FirstName,
                    LastName = addressDto.LastName
                };
            }
            else
            {
                user.Address.Street = addressDto.Street;
                user.Address.City = addressDto.City;
                user.Address.Country = addressDto.Country;
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
            }

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => Error.Failure(e.Code, e.Description)).ToList();
                return Result<AddressDto>.Failure(errors);
            }

            return Result<AddressDto>.Success(addressDto);
        }
    }
}
