using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryKbkDefaultsRequestDto
    {
        [IdIntValue]
        public int? KbkId { get; set; }

        [RequiredValue]
        [EnumValue(EnumType = typeof(BudgetaryAccountCodes))]
        public BudgetaryAccountCodes AccountCode { get; set; }

        [DateValue]
        public DateTime? Date { get; set; }

        public int? TradingObjectId { get; set; }

        [RequiredValue]
        public BudgetaryPeriodSaveDto Period { get; set; }
    }
}
