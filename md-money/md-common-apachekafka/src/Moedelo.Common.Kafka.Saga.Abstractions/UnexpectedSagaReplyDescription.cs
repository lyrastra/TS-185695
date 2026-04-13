using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;

namespace Moedelo.Common.Kafka.Saga.Abstractions
{
    public sealed class UnexpectedSagaReplyDescription
    {
        public UnexpectedSagaReplyDescription(
            string currentStateType, 
            string replyType, 
            string currentStateData, 
            MoedeloEntityCommandReplyKafkaMessageValue replyMessageValue)
        {
            CurrentStateType = currentStateType;
            ReplyType = replyType;
            CurrentStateData = currentStateData;
            ReplyMessageValue = replyMessageValue;
        }

        public string CurrentStateType { get; }
        
        public string ReplyType { get; }
        
        public string CurrentStateData { get; }
        
        public MoedeloEntityCommandReplyKafkaMessageValue ReplyMessageValue { get; }
    }
}