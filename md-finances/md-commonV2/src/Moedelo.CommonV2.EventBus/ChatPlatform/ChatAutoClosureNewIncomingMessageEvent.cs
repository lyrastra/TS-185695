using System;

namespace Moedelo.CommonV2.EventBus.ChatPlatform
{
    public class ChatAutoClosureNewIncomingMessageEvent : AbstractStampedEvent
    {
        public Guid RequestId { get; set; }
    }
}
