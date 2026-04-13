using System;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies
{
    internal sealed class MoedeloEntityCommandReplyKafkaMessageDefinition<T> : MoedeloKafkaMessageDefinitionBase, IMoedeloEntityCommandReplyKafkaMessageDefinition
        where T : IEntityCommandReplyData
    {
        public MoedeloEntityCommandReplyKafkaMessageDefinition(
            string topicName,
            EntityCommandSender sender,
            T replyData)
        {
            if (string.IsNullOrWhiteSpace(topicName))
            {
                throw new ArgumentNullException(nameof(topicName));
            }
            
            if (sender == null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            if (replyData == null)
            {
                throw new ArgumentNullException(nameof(replyData));
            }

            TopicName = topicName;
            Sender = sender;
            KeyMessage = sender.SenderId.ToString();
            ReplyType = EntityCommandReplyTypeMapper.GetCommandReplyType(replyData);
            ReplyData = replyData.ToJsonString();
        }
        
        public EntityCommandSender Sender { get; }
        
        public string ReplyType { get; }
        
        public string ReplyData { get; }
    }
}