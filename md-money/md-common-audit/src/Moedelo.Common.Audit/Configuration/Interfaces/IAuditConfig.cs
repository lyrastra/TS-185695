using Moedelo.Common.Audit.Abstractions.Models;

namespace Moedelo.Common.Audit.Configuration.Interfaces
{
    internal interface IAuditConfig
    {
        /// <summary>
        /// Имя приложения в AuditTrail
        /// </summary>
        string AppName { get; }

        bool IsEnabled();

        bool IsEnabled(AuditSpanType type);
    }
}