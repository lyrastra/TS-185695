using Moedelo.Common.Audit.Abstractions.Interfaces;

namespace Moedelo.Common.Http.Abstractions.Extensions
{
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
}
