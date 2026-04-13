using System.Threading;
using System.Threading.Tasks;
using System.Web;

using Moedelo.CommonV2.Auth.Domain;
using Moedelo.CommonV2.Auth.Private.Extensions;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.CommonV2.Auth.Private;

[InjectAsSingleton(typeof(PrivateAuthenticationService))]
public class PrivateAuthenticationService : IAuthenticationService
{
    private static AuthenticationInfo Authenticate()
    {
        var context = HttpContext.Current;
        var parameters = context.Request.Params;

        try
        {
            var userId = parameters.GetIntValue("userId", defaultValue: 0);
            var firmId = parameters.GetIntValue("firmId", defaultValue: 0);

            if (userId <= 0 && firmId <= 0)
            {
                return null;
            }

            return new AuthenticationInfo(userId, firmId);
        }
        catch
        {
            return null;
        }
    }

    public Task<AuthenticationInfo> AuthenticateAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(Authenticate());
    }
}