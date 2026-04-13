using System.Threading;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.TaxPostings.Kafka.Abstractions;
using Moedelo.TaxPostings.Kafka.Abstractions.Commands;
using Moedelo.TaxPostings.Kafka.Abstractions.Commands.Osno;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Base;

namespace Moedelo.TaxPostings.Kafka.Commands
{
    [InjectAsSingleton(typeof(IOsnoTaxPostingsCommandWriter))]
    internal sealed class OsnoTaxPostingsCommandWriter : MoedeloKafkaTopicWriterBase, IOsnoTaxPostingsCommandWriter
    {
        public OsnoTaxPostingsCommandWriter(
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
        }

        public Task WriteAsync(OverwriteOsnoTaxPostingsCommand command, CancellationToken cancellationToken)
        {
            var key = command.DocumentBaseId.ToString();
            var value = new ODCommandMessageValue
            {
                CommandType = PostingsCommandType.Overwrite,
                CommandDataJson = command.ToJsonString()
            };

            return WriteAsync(TaxPostingsTopics.Commands.OsnoOD, key, value, cancellationToken);
        }

        public Task WriteAsync(DeleteTaxPostingsCommand command, CancellationToken cancellationToken)
        {
            var key = command.DocumentBaseId.ToString();
            var value = new ODCommandMessageValue
            {
                CommandType = PostingsCommandType.Delete,
                CommandDataJson = command.ToJsonString()
            };

            return WriteAsync(TaxPostingsTopics.Commands.OsnoOD, key, value, cancellationToken);
        }
    }
}
