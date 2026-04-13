using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using Moedelo.Common.Enums.Enums.ExternalPartner;
using Moedelo.CommonV2.Auth.Wsse.Domain.Interfaces;
using Moedelo.CommonV2.Auth.Wsse.Helpers;
using Moedelo.InfrastructureV2.Domain.Interfaces.Context;
using Moedelo.InfrastructureV2.Domain.Interfaces.Http;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.CommonV2.Auth.Wsse.WebApi
{
    public class WsseAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        private const string Tag = nameof(WsseAuthenticationAttribute);
        protected const string Method = "X-WSSE";

        public List<ExternalPartnerRule> Rules { get; set; }

        public WsseAuthenticationAttribute(params ExternalPartnerRule[] rules)
        {
            Rules = rules.ToList();
        }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            await OnBeforeAuthenticateAsync(context, cancellationToken).ConfigureAwait(false);

            IExternalPartnerService externalPartnerService = context.Request.GetDependencyScope().GetService(typeof(IExternalPartnerService)) as IExternalPartnerService;
            IHttpEnviroment httpEnviroment = context.Request.GetDependencyScope().GetService(typeof(IHttpEnviroment)) as IHttpEnviroment;

            IEnumerable<string> values;

            if (context.Request.Headers.TryGetValues(Method, out values) && externalPartnerService != null)
            {
                var value = values.FirstOrDefault();

                if (string.IsNullOrWhiteSpace(value))
                {
                    context.ErrorResult = new AuthenticationFailureResult("Missing credentials", context.Request);
                    return;
                }

                try
                {
                    var credentials = await externalPartnerService.GetCredentialAsync().ConfigureAwait(false);
                    var result = WsseHelper.Check(value, credentials, Rules, ComputeHash);

                    if (!string.IsNullOrWhiteSpace(result.Error))
                    {
                        context.ErrorResult = new AuthenticationFailureResult(result.Error, context.Request);
                        return;
                    }

                    if (!string.IsNullOrWhiteSpace(result.InvalidDataError)) 
                    {
                        context.ErrorResult = new BadRequestResult(context.Request);

                        var logger = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ILogger)) as ILogger;
                        var auditContext = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IAuditContext)) as IAuditContext; 

                        logger?.Error(Tag, "Invalid X-WSSE header " + result.InvalidDataError, context: auditContext);

                        return;
                    }

                    var claims = new List<Claim>
                    {
                        new Claim("UserName", result.Partner?.UserName),
                        new Claim(ClaimsIdentity.DefaultNameClaimType, result.Partner?.UserName)
                    };

                    var identity = new ClaimsIdentity(claims, Method);

                    context.Principal = new ClaimsPrincipal(identity);

                    if (result.Partner?.Rules != null && result.Partner.Rules.Any())
                    {
                        httpEnviroment?.ItemList.Add("PartnerRules", result.Partner.Rules);
                        httpEnviroment?.ItemList.Add("PartnerId", result.Partner?.Id);
                    }
                }
                catch (Exception)
                {
                    context.ErrorResult = new InternalServerErrorResult(context.Request);
                    throw;
                }
            }
            else
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing credentials", context.Request);
            }
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var challenge = new AuthenticationHeaderValue(Method);
            context.Result = new AddChallengeOnUnauthorizedResult(challenge, context.Result);
            return Task.FromResult(0);
        }

        public virtual bool AllowMultiple
        {
            get { return false; }
        }

        protected virtual Task OnBeforeAuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected virtual string ComputeHash(string input)
        {
            using (SHA1Managed shHash = new SHA1Managed())
            {
                return Convert.ToBase64String(shHash.ComputeHash(Encoding.UTF8.GetBytes(input)));
            }
        }
    }
}
