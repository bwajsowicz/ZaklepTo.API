using Microsoft.AspNetCore.Builder;
using ExceptionHandlerMiddleware = ZaklepTo.API.Middleware.ExceptionHandlerMiddleware;

namespace ZaklepTo.API.Extensions
{
    public static class Extensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware(typeof(ExceptionHandlerMiddleware));
    }
}