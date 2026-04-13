using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Models;

namespace Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

public interface IAuditTracer
{
    /// <summary>
    /// Подождать, когда конфигурационные данные будут загружены хотя бы один раз.
    /// До этого момента данные будут пусты.
    /// </summary>
    Task WaitForConfigurationReadyAsync();
    
    bool IsAuditTrailOn();
        
    IAuditSpanBuilder BuildSpan(
        AuditSpanType type,
        string spanName = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);
}