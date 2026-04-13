using System;

namespace Moedelo.CommonV2.EventBus.ChatPlatform.RequestContext
{
    public class FillBillingContextCommand
    {
        public Guid RequestId { get; set; }

        public int FirmId { get; set; }
    }
}
