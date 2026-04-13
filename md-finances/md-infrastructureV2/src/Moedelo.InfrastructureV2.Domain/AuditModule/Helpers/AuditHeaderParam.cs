namespace Moedelo.InfrastructureV2.Domain.AuditModule.Helpers;

public static class AuditHeaderParam
{
    /// <summary>
    /// Заголовок для передачи родительского контекста аудита в запросе
    /// </summary>
    public const string ParentScopeContext = "__MdAuditParentScopeContext";

    /// <summary>
    /// Заголовок для передачи контекста аудита в ответе
    /// </summary>
    public const string AuditTrailContext = "MD-AuditTrail-Context";
}