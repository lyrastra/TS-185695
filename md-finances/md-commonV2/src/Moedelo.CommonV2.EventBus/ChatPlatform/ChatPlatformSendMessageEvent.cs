using System;

namespace Moedelo.CommonV2.EventBus.ChatPlatform
{
    public class ChatPlatformSendMessageEvent
    {
        public Guid MessageId { get; set; }

        public string Messenger { get; set; }
    }
}
