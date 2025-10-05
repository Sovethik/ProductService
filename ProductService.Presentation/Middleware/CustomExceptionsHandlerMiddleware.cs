using FluentValidation;
using ProductService.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;
using System.Diagnostics;

namespace ProductService.Presentation.Middleware
{
    public class CustomExceptionsHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionsHandlerMiddleware> _logger;
        private Stopwatch elapsedMs;

        public CustomExceptionsHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionsHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            elapsedMs = new Stopwatch();
        }
           

        public async Task Invoke(HttpContext context)
        {
            try
            {
                elapsedMs = Stopwatch.StartNew();
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandlerExceptionAsync(context, ex);
            }
        }


        public Task HandlerExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;


            switch(ex)
            {
                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    result = JsonSerializer.Serialize(ex.Message);
                    break;
                case ValidationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(ex.Message);
                    break;

            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;


            if(context.Response.StatusCode >= 500)
            {
                elapsedMs.Stop();

                var request = new
                {
                    Method = context.Request.Method,
                    Path = context.Request.Path,
                    StatusCode = context.Response.StatusCode,
                    Elapsed = elapsedMs.ElapsedMilliseconds

                };
                _logger.LogError("HTTP {Method} {PathRequest} with status {StatusCode} in {Elapsed}ms",
                    request.Method, request.Path, request.StatusCode, request.Elapsed);
            }

            //if(result == string.Empty)
            //    result = JsonSerializer.Serialize(ex.Message);

            return context.Response.WriteAsync(result);
        }
    }
}
