using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.AccountingV2.Dto.BizToAccMoneySource
{
    public class BizPaymentDocumentDto
    {
        public long Id { get; set; }

        public decimal Sum { get; set; }

        public PaymentDocumentType Type { get; set; }
    }
}
