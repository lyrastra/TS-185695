using System;
using Moedelo.Accounting.Enums;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Ndfl6ReportInitialData
{
    public class Ndfl6ReportInitialNdflPaymentDto
    {
        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public string DocumentNumber { get; set; }

        public DocumentType DocumentType { get; set; }
    }
}
