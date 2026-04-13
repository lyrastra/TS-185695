using Moedelo.Common.Enums.Enums.KbkNumbers;
using System;

namespace Moedelo.Money.Dto.PaymentOrders.Outgoing.BudgetaryPayment.UnifiedBudgetaryPayment
{
    public class BudgetaryKbkAutocompleteRequestDto
    {
        public string Query { get; set; }

        public BudgetaryAccountCodes AccountCode { get; set; }

        public KbkPaymentType? PaymentType { get; set; }

        public BudgetaryPeriodSaveDto Period { get; set; }

        public DateTime Date { get; set; }
    }
}
