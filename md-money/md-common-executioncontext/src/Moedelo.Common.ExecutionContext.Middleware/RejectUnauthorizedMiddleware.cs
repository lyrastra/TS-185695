using Microsoft.AspNetCore.Http;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using System.Net;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Microsoft.AspNetCore.Authorization;

namespace Moedelo.Common.ExecutionContext.Middleware
{
    internal sealed class RejectUnauthorizedMiddleware
    {
        private readonly RequestDelegate next;

        public RejectUnauthorizedMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context, IExecutionInfoContextAccessor contextAccessor)
        {
            if (context.GetEndpoint()?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                return next(context);
            }

            var executionContext = contextAccessor.ExecutionInfoContext;
            if (executionContext == null ||
                executionContext.FirmId == FirmId.Unidentified &&
                executionContext.UserId == UserId.Unidentified)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return context.Response.WriteAsync("");
            }
            return next(context);
        }
    }
}