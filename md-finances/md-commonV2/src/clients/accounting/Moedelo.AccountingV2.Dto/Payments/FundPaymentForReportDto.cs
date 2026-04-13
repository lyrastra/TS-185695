using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Payroll;

namespace Moedelo.AccountingV2.Dto.Payments
{
    public class FundPaymentForReportDto
    {
        public long Id { get; set; }

        public string PeriodDate { get; set; }

        public decimal Sum { get; set; }

        public string OrderNumber { get; set; }

        public string OrderDate { get; set; }

        public string DocumentTypeDescription { get; set; }
        
        public PaysForEmployeeType PaymentType { get; set; }
        
        public BudgetaryPeriodType PeriodType { get; set; }

        public int PeriodNumber { get; set; }

        public long? DocumentBaseId { get; set; }
        
        public int? KbkId { get; set; }
        
        public string KbkNumber { get; set; }
    }
}