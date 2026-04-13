using System;
using Moedelo.AccountingV2.Dto.Requsites;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.FinancialOperations;
using Moedelo.Common.Enums.Enums.KbkNumbers;

namespace Moedelo.AccountingV2.Dto.Payments
{
    public class CreatePaymentDto
    {
        public int Number { get; set; }
        public DateTime? Date { get; set; }
        public int PeriodNumber { get; set; }
        public int PeriodYear { get; set; }
        public BudgetaryPeriodType PeriodType { get; set; }

        public decimal Sum { get; set; }
        public decimal? BizUsnSum { get; set; }
        public decimal? BizEnvdSum { get; set; }

        public string Description { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Common.Enums.Enums.FinancialOperations.BudgetaryPaymentType BizBudgetaryPaymentType { get; set; }
        public BudgetaryPaymentSubtype BizBudgetaryPaymentSubtype { get; set; }
        public Common.Enums.Enums.Accounting.BudgetaryPaymentType AccountingBudgetaryPaymentType { get; set; }
        public BudgetaryFundType? KontragenType { get; set; }
        public int AccountingTaxesAndFees { get; set; }

        public KbkType AccountingKbkType { get; set; }
        public KbkNumberType? AccountingKbkNumberType { get; set; }

        public DocumentStatus? Status { get; set; }
        public BudgetaryPaymentBase? PaymentFoundation { get; set; }
        public BankKontragentRequisites KontragentRequisites { get; set; }
        public string TaxCode { get; set; }
        public string Kbk { get; set; }
        public int? SettlementAccountId { get; set; }
    }
}