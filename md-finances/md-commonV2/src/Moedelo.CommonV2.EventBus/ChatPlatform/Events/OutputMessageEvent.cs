using System;

namespace Moedelo.CommonV2.EventBus.ChatPlatform.Events
{
    public class OutputMessageEvent
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
    }
}
