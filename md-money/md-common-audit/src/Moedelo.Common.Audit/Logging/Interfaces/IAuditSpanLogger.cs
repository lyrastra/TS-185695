using Moedelo.Common.Audit.Abstractions.Interfaces;

namespace Moedelo.Common.Audit.Logging.Interfaces
{
    internal interface IAuditSpanLogger
    {
        void FireAndForgetLog(IAuditSpanData span);
    }
}