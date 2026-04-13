using System;

namespace Moedelo.CommonV2.EventBus.ChatPlatform.Events
{
    public class ChangedQueueEvent
    {
        public Guid RequestId { get; set; }
        public Guid QueueId { get; set; }
    }
}
