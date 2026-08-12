
using ECommerce.Application.Common;
using ECommerce.Application.DTO_s.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using static ECommerce.Application.Common.ResultOfT;

namespace ECommerce.Application.Contracts
{
    public interface IIdentityService
    {
        Task<Result<IdentityUserResult>> FindByEmailAsync(string email, CancellationToken ct = default);

        Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken ct = default);

        Task<Result<IdentityUserResult>> CreateUser(RegisterDto registerDto, CancellationToken ct = default);

        Task<Result<IEnumerable<string>>> GetRolesAsync(string email, CancellationToken ct = default);

        Task<Result<AddressDto>> GetAddressByEmailAsync(string email, CancellationToken ct = default);

        Task<Result<AddressDto>> UpdateAddressByEmailAsync(string email, AddressDto addressDto, CancellationToken ct = default);

        Task<Result<bool>> EmailExistsAsync(string email, CancellationToken ct = default);
    }
}
