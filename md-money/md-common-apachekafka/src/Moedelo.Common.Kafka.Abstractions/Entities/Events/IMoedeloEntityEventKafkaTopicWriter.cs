using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Events
{
    public interface IMoedeloEntityEventKafkaTopicWriter
    {
        Task<string> WriteEventDataAsync<T>(IMoedeloEntityEventKafkaMessageDefinition<T> eventDefinition);
        Task<string> WriteEventDataAsync<T>(
            IMoedeloEntityEventKafkaMessageDefinition<T> eventDefinition,
            CancellationToken cancellationToken);
        Task<string> WriteEventDataAsync<T>(IMoedeloEntityEventKafkaMessageDefinition<T> eventDefinition,
            ProducingSettings settings,
            CancellationToken cancellationToken);
    }
}