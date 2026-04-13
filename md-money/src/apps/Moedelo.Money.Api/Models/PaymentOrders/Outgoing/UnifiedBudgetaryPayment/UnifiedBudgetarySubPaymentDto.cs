using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;
using System.ComponentModel;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetarySubPaymentDto
    {

        /// <summary>
        /// Идентификатор КБК
        /// </summary>
        [IdIntValue]
        [RequiredValue]
        public int KbkId { get; set; }

        /// <summary>
        /// Бюджетный период
        /// </summary>
        [RequiredValue]
        public BudgetaryPeriodSaveDto Period { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Идентификатор торгового объекта
        /// </summary>
        [IdIntValue]
        [DefaultValue(null)]
        public int? TradingObjectId { get; set; }

        /// <summary>
        /// Идентификатор патента
        /// </summary>
        [IdLongValue]
        [DefaultValue(null)]
        public long? PatentId { get; set; }

    }
}
