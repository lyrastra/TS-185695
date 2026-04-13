using System;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.FinancialOperations;
using BudgetaryPaymentType = Moedelo.Common.Enums.Enums.FinancialOperations.BudgetaryPaymentType;

namespace Moedelo.AccountingV2.Dto.PaymentOrder
{
    public class BudgetaryPaymentDto
    {
            public long Id { get; set; }
            public DateTime DateValue { get; set; }
            public decimal Sum { get; set; }
            public BudgetaryPaymentType BudgetaryPaymentType { get; set; }
            public BudgetaryPaymentSubtype BudgetaryPaymentSubtype { get; set; }
            public string Description { get; set; }

            /// <summary> номер квартала, к которому "привязан" платёж </summary>
            public int Quarter { get; set; }
            public string Kbk { get; set; }
            public BudgetaryPeriodType PeriodType { get; set; }
            public int PeriodYear { get; set; }
    }
}