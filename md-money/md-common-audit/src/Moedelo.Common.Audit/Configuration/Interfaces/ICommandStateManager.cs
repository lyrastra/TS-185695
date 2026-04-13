using Moedelo.Common.Audit.Configuration.Models;

namespace Moedelo.Common.Audit.Configuration.Interfaces
{
    internal interface ICommandStateManager
    {
        /// <summary>
        /// Имя приложения в AuditTrail
        /// </summary>
        string AuditTrailAppName { get; }

        CommandState Current { get; }
    }
}