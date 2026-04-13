using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Context;

namespace Moedelo.CommonV2.UserContext;

[InjectPerWebRequest(typeof(ICustomAuditContext))]
public class CustomAuditContext : ICustomAuditContext
{
    public int? FirmId { get; set; }

    public int? UserId { get; set; }
}