using System;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.KbkNumbers;

namespace Moedelo.AccountingV2.Dto.CashOrder
{
    public class BudgetaryCashOrderDto
    {
        public int? BudgetaryTaxesAndFees { get; set; }

        public long CashId { get; set; }

        public string Date { get; set; }

        public string Destination { get; set; }

        public string FirmName { get; set; }

        public long Id { get; set; }

        public int? KbkId { get; set; }

        public KbkPaymentType? KbkPaymentType { get; set; }

        public string KontragentName { get; set; }

        public string Number { get; set; }

        public BudgetaryPeriodType PeriodType { get; set; }

        public DateTime? BudgetaryPeriodDate { get; set; }

        public int Year { get; set; }

        public int? PeriodNumber { get; set; }

        public decimal Sum { get; set; }

        public long? PatentId { get; set; }
        
        public virtual TaxationSystemType? TaxationSystemType { get; set; }
    }
}