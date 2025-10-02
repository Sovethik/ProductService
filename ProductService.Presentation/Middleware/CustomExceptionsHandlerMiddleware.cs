using FluentValidation;
using ProductService.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace ProductService.Presentation.Middleware
{
    public class CustomExceptionsHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionsHandlerMiddleware(RequestDelegate next)
            => _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
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

            if(result == string.Empty)
                result = JsonSerializer.Serialize(ex.Message);

            return context.Response.WriteAsync(result);
        }
    }
}
