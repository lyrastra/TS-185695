using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;

namespace Moedelo.Common.Audit.Writers.Kafka
{
    internal interface IAuditKafkaWriter
    {
        Task WriteAsync(IReadOnlyCollection<IAuditSpanData> rows);
    }
}
