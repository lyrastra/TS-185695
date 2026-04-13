using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.TaxPostings.Kafka.Abstractions.Commands.Patent
{
    public interface IPatentTaxPostingsCommandWriter
    {
        Task WriteAsync(OverwritePatentTaxPostingsCommand command, CancellationToken cancellationToken = default);
        Task WriteAsync(DeleteTaxPostingsCommand command, CancellationToken cancellationToken = default);
    }
}
