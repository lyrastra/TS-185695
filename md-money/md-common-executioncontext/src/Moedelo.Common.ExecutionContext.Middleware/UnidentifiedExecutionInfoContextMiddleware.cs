using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Common.ExecutionContext.Client;

namespace Moedelo.Common.ExecutionContext.Middleware
{
    internal sealed class UnidentifiedExecutionInfoContextMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;
        private readonly IExecutionContextApiClient executionContextApiClient;
        private readonly IExecutionInfoContextInitializer contextInitializer;
        private readonly IExecutionInfoContextAccessor contextAccessor;
        
        public UnidentifiedExecutionInfoContextMiddleware(
            RequestDelegate next,
            ILogger<UnidentifiedExecutionInfoContextMiddleware> logger,
            IExecutionContextApiClient executionContextApiClient,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor)
        {
            this.next = next;
            this.logger = logger;
            this.executionContextApiClient = executionContextApiClient;
            this.contextInitializer = contextInitializer;
            this.contextAccessor = contextAccessor;
        }
        
        public async Task InvokeAsync(HttpContext httpContext)
        {
            string token;
            ExecutionInfoContext executionInfoContext = null;
            try
            {
                token = await executionContextApiClient.GetUnidentifiedTokenAsync();
                executionInfoContext = contextInitializer.Initialize(token);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await httpContext.Response.WriteAsync("");
                return;
            }

            await contextAccessor.RunInContextAsync(token, executionInfoContext,
                static (nextRef, httpContextRef) => nextRef(httpContextRef),
                next, httpContext);
        }
    }
}