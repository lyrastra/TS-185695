using System;
using Moedelo.Payroll.Enums.Ndfl;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Ndfl6ReportInitialData
{
    public class Ndfl6ReportInitialIncomeDto
    {
        public int WorkerId { get; set; }
        public NdflRateType NdflRate { get; set; }
        public int Code { get; set; }
        public decimal Sum { get; set; }
        public DateTime Date { get; set; }
        public int TaxSum { get; set; }
        public DateTime? TaxDate { get; set; }
        public DateTime? PaymentDeadlineDate { get; set; }
    }
}
