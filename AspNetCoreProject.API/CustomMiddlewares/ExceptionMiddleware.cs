using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AspNetCoreProject.API.CustomMiddlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        
        public ExceptionMiddleware(RequestDelegate next, ILogger logger) {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext) {
            try {
                await _next(httpContext);
            }
            catch (Exception ex) {
                //_logger.Log(ex);
                await GlobalExceptionHandler(httpContext, ex);
            }
        }

        private static Task GlobalExceptionHandler(HttpContext httpContext, Exception exception) {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;

            return httpContext.Response.WriteAsync(new ErrorDetails { 
                Message = exception.Message,
                StatusCode = httpContext.Response.StatusCode
            }.ToString());
        }
    }
}