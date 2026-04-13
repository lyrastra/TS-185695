using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Payments
{
    public class DocumentsNumbersDto
    {
        public DocumentsNumbersDto()
        {
            SalaryProjectDocumentNumbers = new List<SalaryProjectDocumentNumberDto>();
        }
        
        public decimal PaybillDocumentNumber { get; set; }
        public decimal PaymentOrderDocumentNumber { get; set; }
        public List<SalaryProjectDocumentNumberDto> SalaryProjectDocumentNumbers { get; set; }
        public decimal CashOrderDocumentNumber { get; set; }
    }
}
