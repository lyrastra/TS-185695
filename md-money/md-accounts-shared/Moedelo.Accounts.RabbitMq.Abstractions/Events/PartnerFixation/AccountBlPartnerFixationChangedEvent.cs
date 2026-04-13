using System;
using System.Collections.Generic;

namespace Moedelo.Accounts.RabbitMq.Abstractions.Events.PartnerFixation
{
    // копия https://gitlab.mdtest.org/development/infra/md-commonV2/-/blob/master/src/Moedelo.CommonV2.EventBus/Account/AccountBlPartnerFixationChangedEvent.cs
    public class AccountBlPartnerFixationChangedEvent
    {
        public static string Exchange => "AccountBlPartnerFixationChanged";

        public List<int> FirmIds { get; set; }
        public int PartnerUserId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
