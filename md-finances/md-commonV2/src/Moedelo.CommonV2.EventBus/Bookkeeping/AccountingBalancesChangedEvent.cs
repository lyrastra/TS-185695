using System;

namespace Moedelo.CommonV2.EventBus.Bookkeeping
{
    /// <summary>
    /// Событие ввода/изменения остатков.
    /// </summary>
    public class AccountingBalancesChangedEvent
    {
        public int FirmId { get; set; }

        public DateTime? Date { get; set; }

        public DateTime EventTimestamp { get; set; }
    }
}
