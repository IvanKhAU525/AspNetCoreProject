using Microsoft.AspNetCore.Builder;

namespace AspNetCoreProject.API.CustomMiddlewares
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app) =>
            app.UseMiddleware<ExceptionMiddleware>();
    }
}