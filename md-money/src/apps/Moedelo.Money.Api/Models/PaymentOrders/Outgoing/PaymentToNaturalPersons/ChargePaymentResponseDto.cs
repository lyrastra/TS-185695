using Moedelo.Infrastructure.AspNetCore.Validation;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public class ChargePaymentResponseDto
    {
        /// <summary>
        /// Начисление в ЗП
        /// </summary>
        [IdLongValue]
        public long ChargeId { get; set; }

        /// <summary>
        /// Связь выплаты с начислением (фиксируется в ЗП)
        /// </summary>
        [IdIntValue]
        public int? ChargePaymentId { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>

        [SumValue]
        public decimal Sum { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
    }
}
