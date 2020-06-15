using Microsoft.AspNetCore.Builder;
using Presentation.WebAPI.Middleware;

namespace Presentation.WebAPI.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}