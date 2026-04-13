using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Kafka.Entities.Events;

[InjectAsSingleton(typeof(IMoedeloEntityEventKafkaTopicWriter))]
internal sealed class MoedeloEntityEventKafkaTopicWriter : MoedeloKafkaTopicWriterBase, IMoedeloEntityEventKafkaTopicWriter
{
    public MoedeloEntityEventKafkaTopicWriter(IMoedeloKafkaTopicWriterBaseDependencies dependencies)
        : base(dependencies)
    {
    }

    public Task<string> WriteEventDataAsync<T>(IMoedeloEntityEventKafkaMessageDefinition<T> eventDefinition)
    {
        return WriteEventDataAsync(eventDefinition, CancellationToken.None);
    }

    public Task<string> WriteEventDataAsync<T>(
        IMoedeloEntityEventKafkaMessageDefinition<T> eventDefinition,
        CancellationToken cancellationToken)
    {
        return WriteEventDataAsync(eventDefinition, ProducingSettings.Default, cancellationToken);
    }

    public Task<string> WriteEventDataAsync<T>(
        IMoedeloEntityEventKafkaMessageDefinition<T> eventDefinition,
        ProducingSettings settings,
        CancellationToken cancellationToken)
    {
        if (eventDefinition == null)
        {
            throw new ArgumentNullException(nameof(eventDefinition));
        }
            
        var messageValue = new MoedeloEntityEventKafkaMessageValue(
            eventDefinition.EntityType, 
            eventDefinition.EventType,
            eventDefinition.EventData);

        return WriteAsync(
            eventDefinition.TopicName,
            eventDefinition.KeyMessage,
            messageValue,
            settings,
            cancellationToken);
    }
}