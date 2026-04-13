using System.Threading;
using System.Threading.Tasks;
using Moedelo.LinkedDocuments.Kafka.Abstractions.BaseDocuments.Commands;

namespace Moedelo.LinkedDocuments.Kafka.Abstractions.BaseDocuments
{
    public interface IBaseDocumentsCommandWriter
    {
        /// <summary>
        /// Устанавливает статус НУ для документа
        /// </summary>
        Task WriteAsync(SetTaxStatusCommand command, CancellationToken cancellationToken = default);
    }
}
