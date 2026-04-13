using System;
using System.Collections.Generic;

namespace Moedelo.CommonV2.EventBus.Requisities
{
    public class RequisitiesFirmEvent
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public RequisitesFields[] ChangedFields { get; set; }
        public Dictionary<RequisitesFields, string> ChangedFieldsValues { get; set; }
    }

    public enum RequisitesFields
    {
        Inn,
        [Obsolete("Use phone changed event")]
        PhoneNumber
    }
}