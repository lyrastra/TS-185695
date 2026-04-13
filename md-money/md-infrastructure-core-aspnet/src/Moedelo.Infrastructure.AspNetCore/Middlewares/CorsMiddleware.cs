using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Moedelo.Infrastructure.AspNetCore.Middlewares
{
    public class CorsMiddleware
    {
        private readonly RequestDelegate next;

        public CorsMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            string originHeader = context.Request.Headers["Origin"];

            context.Response.Headers.Append("Access-Control-Allow-Origin", originHeader ?? "");
            context.Response.Headers.Append("Access-Control-Allow-Credentials", "true");
            context.Response.Headers.Append("Access-Control-Expose-Headers", "Content-Disposition");

            if (context.Request.Method == "OPTIONS")
            {
                context.Response.Headers.Append("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, PATCH");
                context.Response.Headers.Append("Access-Control-Allow-Headers", "Content-Type, Accept, Cache-control, pragma");
                context.Response.Headers.Append("Access-Control-Max-Age", "1728000");
                return context.Response.WriteAsync("");
            }

            return next(context);
        }
    }
}
