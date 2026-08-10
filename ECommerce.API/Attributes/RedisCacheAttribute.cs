using ECommerce.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace ECommerce.API.Attributes
{
    public class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly int cacheDurationInSeconds;

        public RedisCacheAttribute(int cacheDurationInSeconds)
        {
            this.cacheDurationInSeconds = cacheDurationInSeconds;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var cacheKey = GenerateCacheKey(context.HttpContext.Request);

            var cachedResponse = await cacheService.GetAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedResponse))
            {
                context.Result = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            var executed = await next.Invoke();
            if (executed.Result is OkObjectResult { Value: not null } ok)
                await cacheService.SetAsync(cacheKey, ok.Value, TimeSpan.FromSeconds(cacheDurationInSeconds));

            return;
        }

        private static string GenerateCacheKey(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append(request.Path).Append("?");
            foreach (var (key, value) in request.Query.OrderBy(q => q.Key))
            {
                keyBuilder.Append(key).Append("=").Append(value).Append("&");
            }
            return keyBuilder.ToString();
        }
    }
}
