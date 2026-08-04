using AutoMapper;
using ECommerce.Application.DTO_s.Products;
using ECommerce.Domain.Entities.Products;
using Microsoft.Extensions.Options;

namespace ECommerce.Application.Profiles
{
    public class PictureUrlResolver(IOptions<UrlSettings> urlSettings) : IValueResolver<Product, ProductDto, string?>
    {
        private readonly IOptions<UrlSettings> urlSettings = urlSettings;
        public string? Resolve(Product source, ProductDto destination, string? destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return null;

            var baseUrl = urlSettings.Value.BaseUrl.TrimEnd('/');
            var pictureUrl = source.PictureUrl.TrimStart('/');
            return $"{baseUrl}/Files/{pictureUrl}";
        }
    }

    public class UrlSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
    }
}
