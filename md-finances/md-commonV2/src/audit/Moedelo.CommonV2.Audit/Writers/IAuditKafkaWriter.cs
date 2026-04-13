using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.CommonV2.Audit.Writers
{
    public interface IAuditKafkaWriter
    {
        Task WriteAsync(IReadOnlyCollection<IAuditSpan> rows);
    }
}
