using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moedelo.Common.ExecutionContext.Abstractions.Exceptions;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Auth.Authentication.Extensions;
using Moedelo.Common.Jwt.Abstractions;
using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Client;

namespace Moedelo.Common.ExecutionContext.Auth.Authentication.Schemes.Handlers
{
    internal class OAuthAuthenticationHandler : AuthenticationHandler<MoedeloAuthenticationSchemeOptions>
    {
        private const string PrivateJwtHeader = "IsPrivate";

        private readonly IExecutionContextApiClient tokenProvider;
        private readonly IJwtService jwtService;
        private readonly IExecutionInfoContextInitializer contextInitializer;

        public OAuthAuthenticationHandler(
            IOptionsMonitor<MoedeloAuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
#if NET8_0_OR_GREATER
#else
            ISystemClock clock,
#endif
            IExecutionContextApiClient tokenProvider,
            IJwtService jwtService,
            IExecutionInfoContextInitializer contextInitializer)
#if NET8_0_OR_GREATER
            : base(options, logger, encoder)
#else
            : base(options, logger, encoder, clock)
#endif
        {
            this.tokenProvider = tokenProvider;
            this.jwtService = jwtService;
            this.contextInitializer = contextInitializer;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Request.Headers.TryGetValue(Options.HeaderName, out var authorizationHeader) == false)
            {
                return AuthenticateResult.NoResult();
            }

            var bearerAuthHeader = authorizationHeader.Where(x => x.StartsWith(AuthenticationConstants.BearerPrefix, StringComparison.OrdinalIgnoreCase))
                .ToArray()
                .FirstOrDefault();
            if (string.IsNullOrWhiteSpace(bearerAuthHeader))
            {
                return AuthenticateResult.NoResult();
            }

            var publicToken = bearerAuthHeader.Substring(AuthenticationConstants.BearerPrefix.Length);
            if (string.IsNullOrWhiteSpace(publicToken))
            {
                return AuthenticateResult.Fail("Missing Bearer token");
            }

            var headers = jwtService.Headers(publicToken);
            if (headers.ContainsKey(PrivateJwtHeader))
            {
                return SuccessResult(publicToken);
            }

            // check guid

            var companyId = Request.GetCompanyIdParam();
            var privateToken = await tokenProvider.GetTokenFromPublicAsync(publicToken, companyId).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(privateToken))
            {
                return AuthenticateResult.Fail("Invalid Bearer token");
            }

            return SuccessResult(privateToken);
        }

        private AuthenticateResult SuccessResult(string privateToken)
        {
            try
            {
                contextInitializer.Initialize(privateToken);
            }
            catch (ExecutionContextInitializationFailedException ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }

            var ticket = Scheme.GetTicket();
            return AuthenticateResult.Success(ticket);
        }
    }
}
