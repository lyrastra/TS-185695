using System;
using Moedelo.Common.Kafka.Abstractions.Base;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies
{
    public sealed class MoedeloEntityCommandReplyKafkaMessageValue : MoedeloKafkaMessageValueBase
    {
        public MoedeloEntityCommandReplyKafkaMessageValue(
            EntityCommandSender sender,
            string replyType, 
            string replyData)
        {
            if (string.IsNullOrWhiteSpace(replyType))
            {
                throw new ArgumentNullException(nameof(replyType));
            }
            
            if (string.IsNullOrWhiteSpace(replyData))
            {
                throw new ArgumentNullException(nameof(replyData));
            }
            
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
            ReplyType = replyType;
            ReplyData = replyData;
        }
        
        public EntityCommandSender Sender { get; }
        
        public string ReplyType { get; }
        
        public string ReplyData { get; }
    }
}