using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Commands
{
    public sealed class CommandWithReplyData<T> : IEntityCommandData
        where T : IEntityCommandData
    {
        static CommandWithReplyData()
        {
            EntityCommandTypeMapper.AddTypeMap<CommandWithReplyData<T>>($"CommandWithReplyData_{typeof(T)}");
        }
        
        public CommandWithReplyData(T data, ReplyTo replyTo)
        {
            Data = data ?? throw new ArgumentException(nameof(data));
            ReplyTo = replyTo ?? throw new ArgumentException(nameof(replyTo));
        }

        public void Deconstruct(out T data, out ReplyTo replyTo)
        {
            data = Data;
            replyTo = ReplyTo;
        }

        public T Data { get; }

        public ReplyTo ReplyTo { get; }
    }
}