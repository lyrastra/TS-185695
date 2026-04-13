using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Moedelo.Infrastructure.AspNetCore.Middlewares;

namespace Moedelo.Infrastructure.AspNetCore.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UsePing(this IApplicationBuilder app)
        {
            return app.Map(
                "/ping",
                applicationBuilder => applicationBuilder
                    .Run(static async httpContext => await httpContext.Response.WriteAsync("pong").ConfigureAwait(false)));
        }

        public static IApplicationBuilder UsePing(this IApplicationBuilder app, Func<HttpContext, Task> extraAction)
        {
            return app.Map(
                "/ping",
                applicationBuilder => applicationBuilder
                    .Run(async httpContext =>
                    {
                        await extraAction(httpContext).ConfigureAwait(false);

                        await httpContext.Response.WriteAsync("pong").ConfigureAwait(false);
                    }));
        }

        public static IApplicationBuilder UseMoedeloCors(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CorsMiddleware>();
        }

        public static IApplicationBuilder UseDefaultExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}