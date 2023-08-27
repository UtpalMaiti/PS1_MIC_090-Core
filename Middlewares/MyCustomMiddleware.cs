using PS1_MIC_090_Core.Controllers;

namespace PS1_MIC_090_Core.Middlewares
{
    public class MyCustomMiddleware
    {
        private readonly RequestDelegate _next;

        public MyCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // IMessageWriter is injected into InvokeAsync
        public async Task InvokeAsync(HttpContext httpContext, ILogger<MyCustomMiddleware> svc)
        {
            svc.LogInformation(DateTime.Now.Ticks.ToString());
            await _next(httpContext);
        }
    }

    public static class MyCustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyCustomMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyCustomMiddleware>();
        }
    }
}
