using System;

namespace Moedelo.CommonV2.EventBus.ChatPlatform
{
    public class ChatAutoClosureRequestStatusChangedEvent: AbstractStampedEvent
    {
        public Guid RequestId { get; set; }
        public Guid? RequestStatusId { get; set; }
    }
}
