using System.Threading;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.TaxPostings.Kafka.Abstractions;
using Moedelo.TaxPostings.Kafka.Abstractions.Commands;
using Moedelo.TaxPostings.Kafka.Abstractions.Commands.IpOsno;
using System.Threading.Tasks;

namespace Moedelo.TaxPostings.Kafka.Commands
{
    [InjectAsSingleton(typeof(IIpOsnoTaxPostingsCommandWriter))]
    internal sealed class IpOsnoTaxPostingsCommandWriter : MoedeloKafkaTopicWriterBase, IIpOsnoTaxPostingsCommandWriter
    {
        public IpOsnoTaxPostingsCommandWriter(
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
        }

        public Task WriteAsync(OverwriteIpOsnoTaxPostingsCommand command, CancellationToken cancellationToken)
        {
            var key = command.DocumentBaseId.ToString();
            var value = new ODCommandMessageValue
            {
                CommandType = PostingsCommandType.Overwrite,
                CommandDataJson = command.ToJsonString()
            };

            return WriteAsync(TaxPostingsTopics.Commands.IpOsnoOD, key, value, cancellationToken);
        }

        public Task WriteAsync(DeleteTaxPostingsCommand command, CancellationToken cancellationToken)
        {
            var key = command.DocumentBaseId.ToString();
            var value = new ODCommandMessageValue
            {
                CommandType = PostingsCommandType.Delete,
                CommandDataJson = command.ToJsonString()
            };

            return WriteAsync(TaxPostingsTopics.Commands.IpOsnoOD, key, value, cancellationToken);
        }
    }
}
