
using ECommerce.Application.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application
{
    public static class ApplicationServicesRegisteration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(c => c.AddProfile(new ProductProfile)); 
            return services;
        }
    }
}
