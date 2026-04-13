using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Settings;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies
{
    public interface IMoedeloEntityCommandReplyKafkaTopicReader
    {
        Task ReadCommandReplyTopicAsync(
            ReadTopicSetting<MoedeloEntityCommandReplyKafkaMessageValue> settings, 
            CancellationToken cancellationToken);
    }
}