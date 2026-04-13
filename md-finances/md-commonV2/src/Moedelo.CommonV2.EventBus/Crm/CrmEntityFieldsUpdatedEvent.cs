using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Uploaded;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class CrmEntityFieldsUpdatedEvent
    {
        public string Login { get; set; }

        public int? FirmId { get; set; }

        public Dictionary<string, string> Fields { get; set; }

        public DateTime Timestamp { get; set; }
    }
}