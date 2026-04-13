using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryKbkAutocompleteRequestDto
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
