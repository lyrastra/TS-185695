using System.Threading.Tasks;
using Moedelo.CommonV2.Audit.Management.Domain;

namespace Moedelo.CommonV2.Audit.Management;

public interface IAuditTrailTogglesReader
{
    string AppName { get; }

    /// <summary>
    /// Подождать, когда данные будут загружены хотя бы один раз.
    /// До этого момента данные будут пусты.
    /// </summary>
    Task WaitForReadyAsync();

    AuditTrailToggles Current { get; }
}