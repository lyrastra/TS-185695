using Moedelo.Infrastructure.AspNetCore.Validation;
using System;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetaryPaymentKbkDefaultsRequestDto
    {
        [DateValue]
        public DateTime? Date { get; set; }

        public int? TradingObjectId { get; set; }

        [RequiredValue]
        public BudgetaryPeriodSaveDto Period { get; set; }
    }
}
