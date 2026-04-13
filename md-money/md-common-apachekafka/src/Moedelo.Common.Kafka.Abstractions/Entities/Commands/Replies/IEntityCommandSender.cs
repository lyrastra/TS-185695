using System;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies
{
    public interface IEntityCommandSender
    {
        Guid SenderId { get; }
        
        string SenderType { get; }
    }
}