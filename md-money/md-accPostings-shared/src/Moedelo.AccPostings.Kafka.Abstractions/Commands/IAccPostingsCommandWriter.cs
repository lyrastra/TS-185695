using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.AccPostings.Kafka.Abstractions.Commands
{
    public interface IAccPostingsCommandWriter
    {
        Task WriteAsync(OverwriteAccPostingsV2Command command, CancellationToken cancellationToken = default);
        Task WriteAsync(DeleteAccPostingsCommand command, CancellationToken cancellationToken = default);
    }
}
