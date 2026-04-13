#nullable enable
using Moedelo.CommonV2.Auth.Domain;
using Moedelo.CommonV2.UserContext.Extensions;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Context;
using Moedelo.InfrastructureV2.Domain.Interfaces.Http;

namespace Moedelo.CommonV2.UserContext;

[InjectPerWebRequest(typeof(IWebRequestAuditContext))]
public class WebRequestAuditContext : IWebRequestAuditContext
{
    private readonly IHttpEnviroment httpEnvironment;

    public WebRequestAuditContext(IHttpEnviroment httpEnvironment)
    {
        this.httpEnvironment = httpEnvironment;
    }

    public int? FirmId => httpEnvironment.GetUserClaimIntValue(MoedeloClaimsTypes.FirmId);

    public int? UserId => httpEnvironment.GetUserClaimIntValue(MoedeloClaimsTypes.UserId);

    public int? ClientId => httpEnvironment.GetUserClaimIntValue(MoedeloClaimsTypes.ClientId);
}
