using System;
using System.Collections.Generic;

namespace Moedelo.CommonV2.EventBus.Account
{
    public class AccountBlPartnerFixationChangedEvent
    {
        public List<int> FirmIds { get; set; }

        public int PartnerUserId { get; set; }

        public int OperatorId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}