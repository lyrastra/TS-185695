using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class SubKbkAutocompleteRequestDto
    {
        public string Query { get; set; }

        [RequiredValue]
        [EnumValue(EnumType = typeof(BudgetaryAccountCodes))]
        public BudgetaryAccountCodes AccountCode { get; set; }

        [EnumValue(typeof(KbkPaymentType), AllowNull = true)]
        public KbkPaymentType? PaymentType { get; set; }

        [RequiredValue]
        public BudgetaryPeriodSaveDto Period { get; set; }

        [RequiredValue]
        public DateTime Date { get; set; }
    }
}
