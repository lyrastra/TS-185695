using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Kafka.Entities.Commands.Replies;

[InjectAsSingleton(typeof(IMoedeloEntityCommandReplyKafkaTopicWriter))]
internal sealed class MoedeloEntityCommandReplyKafkaTopicWriter : MoedeloKafkaTopicWriterBase,
    IMoedeloEntityCommandReplyKafkaTopicWriter
{
    public MoedeloEntityCommandReplyKafkaTopicWriter(IMoedeloKafkaTopicWriterBaseDependencies dependencies)
        : base(dependencies)
    {
    }

    public Task<string> WriteCommandReplyDataAsync(IMoedeloEntityCommandReplyKafkaMessageDefinition replyDefinition)
    {
        return WriteCommandReplyDataAsync(replyDefinition, CancellationToken.None);
    }

    public Task<string> WriteCommandReplyDataAsync(IMoedeloEntityCommandReplyKafkaMessageDefinition replyDefinition,
        CancellationToken cancellationToken)
    {
        return WriteCommandReplyDataAsync(replyDefinition, ProducingSettings.Default, cancellationToken);
    }

    public Task<string> WriteCommandReplyDataAsync(
        IMoedeloEntityCommandReplyKafkaMessageDefinition replyDefinition,
        ProducingSettings settings,
        CancellationToken cancellationToken)
    {
        if (replyDefinition == null)
        {
            throw new ArgumentNullException(nameof(replyDefinition));
        }

        var messageValue = new MoedeloEntityCommandReplyKafkaMessageValue(
            replyDefinition.Sender,
            replyDefinition.ReplyType,
            replyDefinition.ReplyData);

        return WriteAsync(
            replyDefinition.TopicName,
            replyDefinition.KeyMessage,
            messageValue,
            settings,
            cancellationToken);
    }
}