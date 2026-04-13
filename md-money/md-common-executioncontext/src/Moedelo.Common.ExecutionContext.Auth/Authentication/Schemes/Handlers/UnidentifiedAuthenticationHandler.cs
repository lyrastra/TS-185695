using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moedelo.Common.ExecutionContext.Abstractions.Exceptions;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Client;

namespace Moedelo.Common.ExecutionContext.Auth.Authentication.Schemes.Handlers
{
    internal class UnidentifiedAuthenticationHandler : AuthenticationHandler<MoedeloAuthenticationSchemeOptions>
    {
        private readonly IExecutionContextApiClient tokenProvider;
        private readonly IExecutionInfoContextInitializer contextInitializer;

        public UnidentifiedAuthenticationHandler(
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
            var privateToken = await tokenProvider.GetUnidentifiedTokenAsync().ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(privateToken))
            {
                return AuthenticateResult.Fail("Unknown error");
            }

            try
            {
                contextInitializer.Initialize(privateToken);
            }
            catch (ExecutionContextInitializationFailedException ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }

            return AuthenticateResult.NoResult();
        }
    }
}
