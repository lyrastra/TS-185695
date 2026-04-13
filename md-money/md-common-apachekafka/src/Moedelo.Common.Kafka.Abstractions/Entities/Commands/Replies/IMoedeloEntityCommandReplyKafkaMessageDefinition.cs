using Moedelo.Common.Kafka.Abstractions.Base;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies
{
    public interface IMoedeloEntityCommandReplyKafkaMessageDefinition: IMoedeloKafkaMessageDefinition
    {
        EntityCommandSender Sender { get; }
        
        string ReplyType { get; }
        
        string ReplyData { get; }
    }
}