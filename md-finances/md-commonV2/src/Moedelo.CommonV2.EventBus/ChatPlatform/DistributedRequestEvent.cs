using System;

namespace Moedelo.CommonV2.EventBus.ChatPlatform
{
    public class DistributedRequestEvent
    {
        public Guid RequestId { get; set; }

        public Guid UserId { get; set; }
    }
}
