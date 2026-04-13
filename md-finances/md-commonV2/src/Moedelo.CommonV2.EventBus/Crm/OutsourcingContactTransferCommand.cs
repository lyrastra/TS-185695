using System.Collections.Generic;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class OutsourcingContactTransferCommand
    {
        public int OutsourcingClientId { get; set; }

        public int FirmId { get; set; }

        public IReadOnlyCollection<int> OutsourcingContactInfoIds { get; set; }
    }
}