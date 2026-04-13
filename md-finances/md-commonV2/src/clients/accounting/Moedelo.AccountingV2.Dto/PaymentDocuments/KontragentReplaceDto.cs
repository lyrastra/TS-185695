using System;

namespace Moedelo.AccountingV2.Dto.PaymentDocuments
{
    public class KontragentReplaceDto
    {
        public KontragentForPaymentDocumentsReplaceDto Dto { get; set; }

        public DateTime StartDate { get; set; }
    }
}
