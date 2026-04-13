using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient.Extensions;

internal static class AuditSpanBuilderExtensions
{
    internal static IAuditSpanBuilder WithFullUri(
        this IAuditSpanBuilder spanBuilder,
        string fullUri)
    {
        spanBuilder.WithTag("Uri", fullUri);
            
        return spanBuilder;
    }
}
