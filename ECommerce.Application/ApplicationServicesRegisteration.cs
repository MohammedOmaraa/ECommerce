
using ECommerce.Application.Contracts;
using ECommerce.Application.Profiles;
using ECommerce.Application.Services;
using ECommerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application
{
    public static class ApplicationServicesRegisteration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(c => c.AddProfile(new ProductProfile()), typeof(ApplicationServicesRegisteration).Assembly);

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IBasketService, BasketService>();

            services.AddSingleton<ICacheService, CacheService>();

            services.AddScoped<IIdentityService, IdentityService>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
