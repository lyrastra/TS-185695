using Moedelo.Common.Audit.Abstractions.Helpers;

namespace Moedelo.Common.Audit.Middleware.Internals;

internal static class CustomHeaderNames
{
    internal const string AuditTrailContext = "MD-AuditTrail-Context";
    internal const string AuditTraceInternalContext = AuditHeaderParamHelper.ParentScopeContext;
}
