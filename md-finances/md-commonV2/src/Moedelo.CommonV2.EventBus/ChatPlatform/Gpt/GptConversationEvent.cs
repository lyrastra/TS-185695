using System;

namespace Moedelo.CommonV2.EventBus.ChatPlatform.Gpt
{
    public class GptConversationEvent
    {
        public Guid RequestId { get; set; }
        public Guid MessageId { get; set; }
        public EventTypeEnum EventType { get; set; }

        public enum EventTypeEnum: byte
        {
            ClientMessage = 1,
            OperatorMessage = 2
        }
    }
}
