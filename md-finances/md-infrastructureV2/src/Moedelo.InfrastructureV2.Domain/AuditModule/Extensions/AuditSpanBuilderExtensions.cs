using Moedelo.InfrastructureV2.Domain.AuditModule.Helpers;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.InfrastructureV2.Domain.AuditModule.Extensions;

public static class AuditSpanBuilderExtensions
{
    public static IAuditSpanBuilder TagCodeSourcePath(
        this IAuditSpanBuilder spanBuilder,
        string memberName,
        string sourceFilePath,
        int sourceLineNumber)
    {
        if (spanBuilder.IsEnabled)
        {
            spanBuilder.WithTag("SourceCode",
                $"{sourceFilePath.NormalizeSourceFilePath()}@{sourceLineNumber} (func {memberName})");
        }

        return spanBuilder;
    }
}
