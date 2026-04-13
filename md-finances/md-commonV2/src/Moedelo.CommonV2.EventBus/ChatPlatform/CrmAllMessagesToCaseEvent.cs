using System;

namespace Moedelo.CommonV2.EventBus.ChatPlatform
{
    public class CrmAllMessagesToCaseEvent
    {
        public string CaseId { get; set; }

        public Guid RequestId { get; set; }
    }
}
