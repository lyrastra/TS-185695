using System;


namespace Moedelo.CommonV2.EventBus.ChatPlatform
{
    public class ChatAutoClosureNewOutcomingMessageEvent : AbstractStampedEvent
    {
        public Guid RequestId { get; set; }
    }
}
