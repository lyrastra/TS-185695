using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;

namespace Moedelo.Common.Kafka.Abstractions.Extensions;

internal static class MoedeloEntityCommandReplyKafkaTopicWriterExtensions
{
    internal static Func<CommandWithReplyData<TCommandData>, Task> WrapCommandWithReply<TCommandData, TReply>(
        this IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
        Func<TCommandData, Task<TReply>> commandWithReply)
        where TCommandData : IEntityCommandData
        where TReply : IEntityCommandReplyData
    {
        return async commandWithReplyData =>
        {
            var (data, replyTo) = commandWithReplyData;
            var reply = await commandWithReply(data).ConfigureAwait(false);

            var replyMessageDefinition = new MoedeloEntityCommandReplyKafkaMessageDefinition<TReply>(
                replyTo.TopicName,
                replyTo.Sender,
                reply);

            await replyWriter.WriteCommandReplyDataAsync(replyMessageDefinition).ConfigureAwait(false);
        };
    }

    internal static Func<CommandWithReplyData<TCommandData>, Task> WrapCommandWithReply<TCommandData, TCommandReplyData>(
        this IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
        Func<TCommandData, ReplyTo, Task<TCommandReplyData>> commandWithReply)
        where TCommandData : IEntityCommandData
        where TCommandReplyData : IEntityCommandReplyData
    {
        return async commandWithReplyData =>
        {
            var (data, replyTo) = commandWithReplyData;
            var reply = await commandWithReply(data, replyTo).ConfigureAwait(false);

            var replyMessageDefinition = new MoedeloEntityCommandReplyKafkaMessageDefinition<TCommandReplyData>(
                replyTo.TopicName,
                replyTo.Sender,
                reply);

            await replyWriter.WriteCommandReplyDataAsync(replyMessageDefinition).ConfigureAwait(false);
        };
    }
}
