using System;

namespace Moedelo.CommonV2.EventBus.ChatPlatform
{
    public class CrmFileToCaseEvent
    {
        public string CaseId { get; set; }

        public Guid AttachmentId { get; set; }
    }
}