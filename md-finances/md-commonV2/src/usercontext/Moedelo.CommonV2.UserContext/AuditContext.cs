using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Context;

namespace Moedelo.CommonV2.UserContext;

[InjectPerWebRequest]
public class AuditContext : IAuditContext
{
    private readonly IWebRequestAuditContext webRequestAuditContext;
    private readonly ICustomAuditContext customAuditContext;

    public AuditContext(
        IWebRequestAuditContext webRequestAuditContext,
        ICustomAuditContext customAuditContext)
    {
        this.webRequestAuditContext = webRequestAuditContext;
        this.customAuditContext = customAuditContext;
    }

    public int? UserId => customAuditContext.UserId ?? webRequestAuditContext.UserId;
    public int? FirmId => customAuditContext.FirmId ?? webRequestAuditContext.FirmId;
}
