using System;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies
{
    public sealed class ReplyTo
    {
        public static ReplyTo Create(Guid senderId, string senderType, string topicName)
        {
            return new ReplyTo(new EntityCommandSender(senderId, senderType), topicName);
        }

        public ReplyTo(EntityCommandSender sender, string topicName)
        {
            Sender = sender ?? throw new ArgumentException(nameof(sender));
            
            if (string.IsNullOrWhiteSpace(topicName))
            {
                throw new ArgumentException(nameof(topicName));
            }

            TopicName = topicName;
        }

        public EntityCommandSender Sender { get; }
        
        public string TopicName { get; }
    }
}