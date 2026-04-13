using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.TaxPostings.Kafka.Abstractions.Commands.Osno
{
    public interface IOsnoTaxPostingsCommandWriter
    {
        Task WriteAsync(OverwriteOsnoTaxPostingsCommand command, CancellationToken cancellationToken = default);
        Task WriteAsync(DeleteTaxPostingsCommand command, CancellationToken cancellationToken = default);
    }
}
