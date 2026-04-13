using System.Threading;
using Moedelo.AccPostings.Kafka.Abstractions;
using Moedelo.AccPostings.Kafka.Abstractions.Commands;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using System.Threading.Tasks;

namespace Moedelo.AccPostings.Kafka.Commands
{
    [InjectAsSingleton(typeof(IAccPostingsCommandWriter))]
    internal sealed class AccPostingsCommandV2Writer : MoedeloKafkaTopicWriterBase, IAccPostingsCommandWriter
    {
        public AccPostingsCommandV2Writer(IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
        }

        public Task WriteAsync(OverwriteAccPostingsV2Command command, CancellationToken cancellationToken)
        {
            var key = command.DocumentBaseId.ToString();
            var value = new ODCommandMessageValue
            {
                CommandType = PostingsCommandType.Overwrite,
                CommandDataJson = command.ToJsonString()
            };

            return WriteAsync(AccPostingsTopics.Commands.PostingsV2OD, key, value, cancellationToken);
        }

        public Task WriteAsync(DeleteAccPostingsCommand command, CancellationToken cancellationToken)
        {
            var key = command.DocumentBaseId.ToString();
            var value = new ODCommandMessageValue
            {
                CommandType = PostingsCommandType.Delete,
                CommandDataJson = command.ToJsonString()
            };

            return WriteAsync(AccPostingsTopics.Commands.PostingsV2OD, key, value, cancellationToken);
        }
    }
}
