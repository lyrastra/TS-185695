using System;
using System.Collections.Generic;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.SalaryPayments
{
    public class DocumentsNumbersDto
    {
        public long PaybillDocumentNumber { get; set; }
        public long PaymentOrderDocumentNumber { get; set; }
        public long CashOrderDocumentNumber { get; set; }
        public IReadOnlyList<SalaryProjectDocumentNumberDto> SalaryProjectDocumentNumbers { get; set; } =
            Array.Empty<SalaryProjectDocumentNumberDto>();
    }
}