
using ECommerce.Application.Contracts;
using ECommerce.Application.Profiles;
using ECommerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application
{
    public static class ApplicationServicesRegisteration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(c => c.AddProfile(new ProductProfile()), typeof(ApplicationServicesRegisteration).Assembly);

            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
