using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moedelo.Common.ExecutionContext.Abstractions.Exceptions;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Auth.Authentication.Extensions;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Client;

namespace Moedelo.Common.ExecutionContext.Auth.Authentication.Schemes.Handlers
{
    internal class CookieAuthenticationHandler : AuthenticationHandler<MoedeloAuthenticationSchemeOptions>
    {
        private const string CookieName = "md-auth";
        private readonly IExecutionContextApiClient tokenProvider;
        private readonly IExecutionInfoContextInitializer contextInitializer;

        public CookieAuthenticationHandler(
            IOptionsMonitor<MoedeloAuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
#if NET8_0_OR_GREATER
#else
            ISystemClock clock,
#endif
            IExecutionContextApiClient tokenProvider,
            IExecutionInfoContextInitializer contextInitializer)
#if NET8_0_OR_GREATER
            : base(options, logger, encoder)
#else
            : base(options, logger, encoder, clock)
#endif
        {
            this.tokenProvider = tokenProvider;
            this.contextInitializer = contextInitializer;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var publicToken = Request.Cookies[CookieName];
            if (string.IsNullOrWhiteSpace(publicToken))
            {
                return AuthenticateResult.NoResult();
            }

            var companyId = Request.GetCompanyIdParam();
            var privateToken = await tokenProvider.GetTokenFromPublicAsync(publicToken, companyId).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(privateToken))
            {
                return AuthenticateResult.Fail("Invalid auth cookie");
            }

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
