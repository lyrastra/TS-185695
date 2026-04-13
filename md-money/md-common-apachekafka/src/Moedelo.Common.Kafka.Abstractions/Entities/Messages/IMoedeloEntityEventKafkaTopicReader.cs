using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Messages
{
    public interface IMoedeloEntityMessageKafkaTopicReader<TMessageValue>
        where TMessageValue : MoedeloKafkaMessageValueBase
    {
        Task ReadFromTopicAsync(
            ReadTopicSetting<TMessageValue> settings,
            CancellationToken cancellationToken);
    }
}