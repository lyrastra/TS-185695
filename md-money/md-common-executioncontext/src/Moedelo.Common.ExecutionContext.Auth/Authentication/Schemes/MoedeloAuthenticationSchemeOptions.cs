using Microsoft.AspNetCore.Authentication;

namespace Moedelo.Common.ExecutionContext.Auth.Authentication.Schemes
{
    public class MoedeloAuthenticationSchemeOptions : AuthenticationSchemeOptions
    {
        public string Scheme { get; set; }
        public string HeaderName { get; set; }
    }
}
