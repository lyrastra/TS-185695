using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Kafka.Entities.Commands;

[InjectAsSingleton(typeof(IMoedeloEntityCommandKafkaTopicWriter))]
internal sealed class MoedeloEntityCommandKafkaTopicWriter : MoedeloKafkaTopicWriterBase, IMoedeloEntityCommandKafkaTopicWriter
{
    public MoedeloEntityCommandKafkaTopicWriter(IMoedeloKafkaTopicWriterBaseDependencies dependencies)
        : base(dependencies)
    {
    }

    public Task<string> WriteCommandDataAsync<T>(IMoedeloEntityCommandKafkaMessageDefinition<T> commandDefinition)
    {
        return WriteCommandDataAsync(commandDefinition, CancellationToken.None);
    }

    public Task<string> WriteCommandDataAsync<T>(
        IMoedeloEntityCommandKafkaMessageDefinition<T> commandDefinition,
        CancellationToken cancellationToken)
    {
        return WriteCommandDataAsync(commandDefinition, ProducingSettings.Default, cancellationToken);
    }

    public Task<string> WriteCommandDataAsync<T>(
        IMoedeloEntityCommandKafkaMessageDefinition<T> commandDefinition,
        ProducingSettings settings,
        CancellationToken cancellationToken)
    {
        if (commandDefinition == null)
        {
            throw new ArgumentNullException(nameof(commandDefinition));
        }

        var messageValue = new MoedeloEntityCommandKafkaMessageValue(
            commandDefinition.EntityType, 
            commandDefinition.CommandType, 
            commandDefinition.CommandData);

        return WriteAsync(
            commandDefinition.TopicName,
            commandDefinition.KeyMessage,
            messageValue,
            settings,
            cancellationToken);
    }

    public async Task QueueToWriteCommandDataListAsync<T>(
        IEnumerable<IMoedeloEntityCommandKafkaMessageDefinition<T>> commandDefinitionList,
        CancellationToken cancellationToken)
    {
        var byTopicName = commandDefinitionList
            .GroupBy(commandDefinition => commandDefinition.TopicName);

        foreach (var commandsToTopic in byTopicName)
        {
            var topicName = commandsToTopic.Key;
            var messageList = commandsToTopic
                .Select(cmd =>
                    new KeyValuePair<string, MoedeloEntityCommandKafkaMessageValue>(
                        cmd.KeyMessage,
                        new MoedeloEntityCommandKafkaMessageValue(
                            cmd.EntityType,
                            cmd.CommandType,
                            cmd.CommandData)));
            await QueueToWriteAsync(topicName, messageList, cancellationToken);
        }
    }
}