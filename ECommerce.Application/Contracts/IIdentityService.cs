
using ECommerce.Application.Common;
using ECommerce.Application.DTO_s.Identity;
using static ECommerce.Application.Common.ResultOfT;

namespace ECommerce.Application.Contracts
{
    public interface IIdentityService
    {
        Task<Result<IdentityUserResult>> FindByEmailAsync(string email, CancellationToken ct = default);

        Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken ct = default);

        Task<Result<IdentityUserResult>> CreateUser(RegisterDto registerDto, CancellationToken ct = default);

    }
}
