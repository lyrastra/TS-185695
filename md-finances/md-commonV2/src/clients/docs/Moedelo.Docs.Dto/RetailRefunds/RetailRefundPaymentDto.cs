using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Docs.Dto.RetailRefunds
{
    public class RetailRefundPaymentDto
    {
        public long DocumentBaseId { get; set; }
        
        public AccountingDocumentType PaymentType { get; set; }
        
        public long? CashierId { get; set; }
    }
}