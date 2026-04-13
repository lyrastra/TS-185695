using System.Threading;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.LinkedDocuments.Kafka.Abstractions.BaseDocuments;
using Moedelo.LinkedDocuments.Kafka.Abstractions.BaseDocuments.Commands;
using System.Threading.Tasks;

namespace Moedelo.LinkedDocuments.Kafka.BaseDocuments.Commands
{
    [InjectAsSingleton(typeof(IBaseDocumentsCommandWriter))]
    internal sealed class BaseDocumentsCommandWriter : MoedeloKafkaTopicWriterBase, IBaseDocumentsCommandWriter
    {
        public BaseDocumentsCommandWriter(
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
        }

        public Task WriteAsync(SetTaxStatusCommand command, CancellationToken cancellationToken)
        {
            var key = command.Id.ToString();
            var value = new SetTaxStatusCommandMessageValue
            {
                Id = command.Id,
                TaxStatus = command.TaxStatus
            };

            return WriteAsync(BaseDocumentsTopics.Commands.SetTaxStatusCommand, key, value, cancellationToken);
        }
    }
}
