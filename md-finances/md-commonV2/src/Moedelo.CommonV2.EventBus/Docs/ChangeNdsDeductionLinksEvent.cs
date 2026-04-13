using System.Collections.Generic;

namespace Moedelo.CommonV2.EventBus.Docs
{
    public class ChangeNdsDeductionLinksEvent
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }

        /// <summary>
        /// Авансовые сч-фактуры, с которыми изменились связи по вычету НДС
        /// </summary>
        public List<long> AdvanceInvoiceBaseIds { get; set; }
    }
}