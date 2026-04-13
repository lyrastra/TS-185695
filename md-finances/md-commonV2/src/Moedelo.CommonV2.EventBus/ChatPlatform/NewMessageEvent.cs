using System;

namespace Moedelo.CommonV2.EventBus.ChatPlatform
{
    public class NewMessageEvent
    {
        public Guid RequestId { get; set; }
        public Guid MessageId { get; set; }
    }
}
