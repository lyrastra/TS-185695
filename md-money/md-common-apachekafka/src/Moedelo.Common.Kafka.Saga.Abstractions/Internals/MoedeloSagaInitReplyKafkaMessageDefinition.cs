using System;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Common.Kafka.Saga.Abstractions.Internals
{
    internal sealed class MoedeloSagaInitReplyKafkaMessageDefinition<T> : MoedeloKafkaMessageDefinitionBase, IMoedeloEntityCommandReplyKafkaMessageDefinition
        where T : ISagaStateData
    {
        public MoedeloSagaInitReplyKafkaMessageDefinition(
            Guid sagaId,
            string topicName,
            string sagaType,
            T sagaStateData)
        {
            if (sagaId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(sagaId));
            }
            
            if (string.IsNullOrWhiteSpace(topicName))
            {
                throw new ArgumentNullException(nameof(topicName));
            }
            
            if (string.IsNullOrWhiteSpace(sagaType))
            {
                throw new ArgumentNullException(nameof(sagaType));
            }

            if (sagaStateData == null)
            {
                throw new ArgumentNullException(nameof(sagaStateData));
            }

            var sender = new EntityCommandSender(sagaId, sagaType);
            var replyData = new SagaInitReplyData<T>(sagaStateData);

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