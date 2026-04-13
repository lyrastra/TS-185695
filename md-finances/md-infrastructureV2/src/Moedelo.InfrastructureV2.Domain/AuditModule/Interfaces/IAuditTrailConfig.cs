using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Models;

namespace Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

public interface IAuditTrailConfig
{
    string AppName { get; }
        
    bool IsEnabled();

    bool IsEnabled(AuditSpanType type);

    /// <summary>
    /// Подождать, когда данные будут загружены хотя бы один раз.
    /// До этого момента данные будут пусты.
    /// </summary>
    Task WaitForReadyAsync();
}