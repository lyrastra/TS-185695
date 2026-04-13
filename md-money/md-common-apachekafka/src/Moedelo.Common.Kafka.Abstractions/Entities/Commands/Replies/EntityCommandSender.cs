using System;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies
{
    public sealed class EntityCommandSender : IEntityCommandSender
    {
        public EntityCommandSender(Guid senderId, string senderType)
        {
            if (senderId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(senderId));
            }
            
            if (string.IsNullOrWhiteSpace(senderType))
            {
                throw new ArgumentNullException(nameof(senderType));
            }
            
            SenderId = senderId;
            SenderType = senderType;
        }


        public Guid SenderId { get; }
        
        public string SenderType { get; }
    }
}