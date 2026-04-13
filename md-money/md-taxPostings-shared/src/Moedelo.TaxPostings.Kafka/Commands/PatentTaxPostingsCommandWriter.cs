using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.TaxPostings.Kafka.Abstractions;
using Moedelo.TaxPostings.Kafka.Abstractions.Commands;
using Moedelo.TaxPostings.Kafka.Abstractions.Commands.Patent;

namespace Moedelo.TaxPostings.Kafka.Commands
{
    [InjectAsSingleton(typeof(IPatentTaxPostingsCommandWriter))]
    internal sealed class PatentTaxPostingsCommandWriter : MoedeloKafkaTopicWriterBase, IPatentTaxPostingsCommandWriter
    {
        public PatentTaxPostingsCommandWriter(
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
        }

        public Task WriteAsync(OverwritePatentTaxPostingsCommand command, CancellationToken cancellationToken)
        {
            var key = command.DocumentBaseId.ToString();
            var value = new ODCommandMessageValue
            {
                CommandType = PostingsCommandType.Overwrite,
                CommandDataJson = command.ToJsonString()
            };

            return WriteAsync(TaxPostingsTopics.Commands.PatentOD, key, value, cancellationToken);
        }

        public Task WriteAsync(DeleteTaxPostingsCommand command, CancellationToken cancellationToken)
        {
            var key = command.DocumentBaseId.ToString();
            var value = new ODCommandMessageValue
            {
                CommandType = PostingsCommandType.Delete,
                CommandDataJson = command.ToJsonString()
            };

            return WriteAsync(TaxPostingsTopics.Commands.PatentOD, key, value, cancellationToken);
        }
    }
}
