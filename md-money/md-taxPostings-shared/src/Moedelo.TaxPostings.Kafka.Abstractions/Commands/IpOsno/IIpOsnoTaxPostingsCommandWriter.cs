using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.TaxPostings.Kafka.Abstractions.Commands.IpOsno
{
    public interface IIpOsnoTaxPostingsCommandWriter
    {
        Task WriteAsync(OverwriteIpOsnoTaxPostingsCommand command, CancellationToken cancellationToken = default);
        Task WriteAsync(DeleteTaxPostingsCommand command, CancellationToken cancellationToken = default);
    }
}
