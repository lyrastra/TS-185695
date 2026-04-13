using System;

namespace Moedelo.CommonV2.EventBus.ChatPlatform
{
    public abstract class AbstractStampedEvent
    {
        public DateTime Stamp { get; set; }
    }
}
