using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.FinancialOperations;
using BudgetaryPaymentType = Moedelo.Common.Enums.Enums.FinancialOperations.BudgetaryPaymentType;

namespace Moedelo.Finances.Dto.Money.Operations
{
    public class UsnBudgetaryPrepaymentDto
    {
        public long Id { get; set; }
        public string OrderDate { get; set; }
        public decimal Sum { get; set; }
        public BudgetaryPaymentType BudgetaryPaymentType { get; set; }
        public BudgetaryPaymentSubtype BudgetaryPaymentSubtype { get; set; }
        public string Description { get; set; }

        /// <summary> номер квартала, к которому "привязан" платёж </summary>
        public int Quarter { get; set; }
        public string Kbk { get; set; }
        public BudgetaryPeriodType PeriodType { get; set; }
        public int PeriodYear { get; set; }

        public IReadOnlyCollection<UnifiedBudgetarySubPaymentDto> SubPayments { get; set; }
    }
}
