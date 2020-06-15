using System;
using System.Net;
using System.Threading.Tasks;
using Domain.Services.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Presentation.WebAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;


        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this._next = next;
            this._logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            // Set correct status code
            var errorDetails = GetApiErrorDetails(e);
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = errorDetails.Status;
            httpContext.Response.ContentType = "application/json";

            return httpContext.Response.WriteAsync(errorDetails.ToString());
        }

        private static ApiError GetApiErrorDetails(Exception e)
        {
            var apiError = new ApiError(); // Defaults to a 500 HTTP Status code

            if (e is InvalidParametersException)
            {
                apiError.Status = (int) HttpStatusCode.BadRequest;
                apiError.Description = HttpStatusCode.BadRequest.ToString();
                apiError.Message = e.Message;
            }
            
            return apiError;
        }
    }
}