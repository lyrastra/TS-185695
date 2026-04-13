using System;
using System.Collections.Generic;

namespace Moedelo.CommonV2.EventBus.ChatPlatform
{
    public class CrmMessagesToCaseEvent
    {
        public string CaseId { get; set; }

        public IReadOnlyCollection<Guid> MessagesIds { get; set; }
    }
}
