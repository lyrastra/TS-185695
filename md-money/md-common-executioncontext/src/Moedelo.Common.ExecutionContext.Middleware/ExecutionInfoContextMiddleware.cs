using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Common.ExecutionContext.Client;
using Moedelo.Common.ExecutionContext.Middleware.Extensions;
using Moedelo.Common.Jwt.Abstractions;

namespace Moedelo.Common.ExecutionContext.Middleware
{
    internal sealed class ExecutionInfoContextMiddleware
    {
        private const string AuthorizationHeader = "Authorization";
        private const string AuthorizationScheme = "Bearer";
        private const string MdAuthCookie = "md-auth";
        private const string CompanyIdParamName = "_companyId";
        private const string MdApiKeyHeader = "md-api-key";
        private const string PrivateJwtHeader = "IsPrivate";

        private readonly RequestDelegate next;
        private readonly ILogger logger;
        private readonly IExecutionContextApiClient executionContextApiClient;
        private readonly IJwtService jwtService;
        private readonly IExecutionInfoContextInitializer contextInitializer;
        private readonly IExecutionInfoContextAccessor contextAccessor;

        public ExecutionInfoContextMiddleware(
            RequestDelegate next,
            ILogger<ExecutionInfoContextMiddleware> logger,
            IExecutionContextApiClient executionContextApiClient,
            IJwtService jwtService,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor)
        {
            this.next = next;
            this.logger = logger;
            this.executionContextApiClient = executionContextApiClient;
            this.jwtService = jwtService;
            this.contextInitializer = contextInitializer;
            this.contextAccessor = contextAccessor;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var token = GetJwtToken(httpContext.Request);
            var companyId = GetCompanyId(httpContext.Request);
            var apiKey = GetApiKey(httpContext.Request);

            ExecutionInfoContext executionInfoContext;
            try
            {
                (token, executionInfoContext) = await InitializeContextAsync(token, companyId, apiKey);

                if (token == null)
                {
                    (token, executionInfoContext) = await InitializeUnidentifiedContextAsync();
                }
            }
            catch (Exception exception)
            {
                logger.LogErrorInExecutionInfoContextInvoke(exception);
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await httpContext.Response.WriteAsync("");
                return;
            }

            await contextAccessor.RunInContextAsync(token, executionInfoContext,
                static (nextRef, httpContextRef) => nextRef(httpContextRef),
                next, httpContext);
        }

        private async Task<(string token, ExecutionInfoContext executionInfoContext)> InitializeUnidentifiedContextAsync()
        {
            var privateToken = await executionContextApiClient.GetUnidentifiedTokenAsync();
            var executionInfoContext = contextInitializer.Initialize(privateToken);

            return (privateToken, executionInfoContext);
        }

        private async Task<(string privateToken, ExecutionInfoContext executionInfoContext)> InitializeContextAsync(
            string token, int? companyId, string apiKey)
        {
            try
            {
                var privateToken = await CreatePrivateTokenAsync(token, companyId, apiKey);
                var executionInfoContext = contextInitializer.Initialize(privateToken);

                return (privateToken, executionInfoContext);
            }
            catch (Exception exception)
            {
                logger.LogTokenAuthorizationFailed(exception, token);

                return (null, null);
            }
        }

        private Task<string> CreatePrivateTokenAsync(string token, int? companyId, string apiKey)
        {
            if (string.IsNullOrWhiteSpace(token) == false)
            {
                var headers = jwtService.Headers(token);
            
                if (headers.ContainsKey(PrivateJwtHeader))
                {
                    return Task.FromResult(token);
                }

                return executionContextApiClient.GetTokenFromPublicAsync(token, companyId);
            }

            if (string.IsNullOrWhiteSpace(apiKey) == false)
            {
                return executionContextApiClient.GetTokenFromApiKeyAsync(apiKey);
            }
            
            return executionContextApiClient.GetUnidentifiedTokenAsync();
        }

        private static string GetJwtToken(HttpRequest request)
        {
            string authHeader = request.Headers[AuthorizationHeader];
            
            if (authHeader != null && authHeader.StartsWith(AuthorizationScheme, StringComparison.OrdinalIgnoreCase))
            {
                return authHeader[(AuthorizationScheme.Length + 1)..].Trim();
            }
            
            return request.Cookies[MdAuthCookie];
        }

        private static int? GetCompanyId(HttpRequest request)
        {
            return request.Query[CompanyIdParamName].Count > 0 &&
                int.TryParse(request.Query[CompanyIdParamName][0], out var companyId)
                ? companyId
                : null;
        }
        
        private static string GetApiKey(HttpRequest request)
        {
            return request.Headers[MdApiKeyHeader];
        }
    }
}