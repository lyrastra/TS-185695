using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Common.ExecutionContext.Middleware
{
    public static class ExecutionInfoContextMiddlewareExtensions
    {
        public static IApplicationBuilder UseExecutionInfoContext(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExecutionInfoContextMiddleware>();
        }

        public static IApplicationBuilder UseWhoAmI(
            this IApplicationBuilder builder)
        {
            return builder.Map("/whoAmI",
                applicationBuilder => applicationBuilder
                    .Run(static async httpContext =>
                {
                    var contextAccessor = httpContext.RequestServices.GetService<IExecutionInfoContextAccessor>();

                    var context = contextAccessor.ExecutionInfoContext;

                    if (context == null)
                    {
                        httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        httpContext.Response.ContentType = MediaTypeNames.Text.Plain;
                        await httpContext.Response.WriteAsync("unknown").ConfigureAwait(false);
                        return;
                    }

                    var responseBody = new
                    {
                        FirmId = (int)context.FirmId,
                        UserId = (int)context.UserId,
                        RoleId = (int)context.RoleId,
                        UserRules = context.UserRules
                    };

                    httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                    httpContext.Response.ContentType = MediaTypeNames.Application.Json;
                    await httpContext.Response.WriteAsync(responseBody.ToJsonString()).ConfigureAwait(false);
                }));
        }

        public static IApplicationBuilder UseUnidentifiedExecutionInfoContext(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UnidentifiedExecutionInfoContextMiddleware>();
        }

        public static IApplicationBuilder UseRejectionOfUnauthorizedRequests(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RejectUnauthorizedMiddleware>();
        }
    }
}