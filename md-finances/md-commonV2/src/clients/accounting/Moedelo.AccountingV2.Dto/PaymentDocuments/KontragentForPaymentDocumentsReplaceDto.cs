using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.PaymentDocuments
{
    public class KontragentForPaymentDocumentsReplaceDto
    {
        public int NewKontragentId { get; set; }
        public string NewKontragentName { get; set; }
        public IReadOnlyCollection<int> OldKontragentIds { get; set; }
    }
}
