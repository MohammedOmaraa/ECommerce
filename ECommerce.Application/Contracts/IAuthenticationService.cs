
using ECommerce.Application.DTO_s.Identity;
using static ECommerce.Application.Common.ResultOfT;

namespace ECommerce.Application.Contracts
{
    public interface IAuthenticationService
    {
        public Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default);

        public Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default);
    }
}
