using ProductService.Presentation.Middleware;

namespace ProductService.Presentation
{
    public static class Extentions
    {
        public static IApplicationBuilder UseCustomExceptionsHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionsHandlerMiddleware>();

            return app;
        }
    }
}
