
using ECommerce.Application.DTO_s.Identity;
using static ECommerce.Application.Common.ResultOfT;

namespace ECommerce.Application.Contracts
{
    public interface IAuthenticationService
    {
        public Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default);

        public Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default);

        public Task<Result<bool>> CheckEmailAsync(string email, CancellationToken ct = default);

        public Task<Result<AddressDto>> GetUserAddressAsync(string email, CancellationToken ct = default);

        public Task<Result<AddressDto>> UpdateUserAddressAsync(AddressDto addressDto, string email, CancellationToken ct = default);

        public Task<Result<UserDto>> GetCurrentUserAsync(string email, CancellationToken ct = default);

    }
}
