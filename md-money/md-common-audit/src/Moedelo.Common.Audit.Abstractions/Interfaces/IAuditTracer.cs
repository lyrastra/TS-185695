using System.Runtime.CompilerServices;
using Moedelo.Common.Audit.Abstractions.Models;

namespace Moedelo.Common.Audit.Abstractions.Interfaces
{
    public interface IAuditTracer
    {
        IAuditSpanBuilder BuildSpan(
            AuditSpanType type,
            string spanName = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);
    }
}