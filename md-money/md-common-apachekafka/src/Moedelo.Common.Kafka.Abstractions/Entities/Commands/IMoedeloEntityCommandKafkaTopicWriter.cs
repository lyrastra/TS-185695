using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Commands
{
    public interface IMoedeloEntityCommandKafkaTopicWriter
    {
        Task<string> WriteCommandDataAsync<T>(IMoedeloEntityCommandKafkaMessageDefinition<T> commandDefinition);

        Task<string> WriteCommandDataAsync<T>(IMoedeloEntityCommandKafkaMessageDefinition<T> commandDefinition,
            CancellationToken cancellationToken);

        Task<string> WriteCommandDataAsync<T>(
            IMoedeloEntityCommandKafkaMessageDefinition<T> commandDefinition,
            ProducingSettings settings,
            CancellationToken cancellationToken);

        Task QueueToWriteCommandDataListAsync<T>(
            IEnumerable<IMoedeloEntityCommandKafkaMessageDefinition<T>> commandDefinitionList,
            CancellationToken cancellationToken);
    }
}