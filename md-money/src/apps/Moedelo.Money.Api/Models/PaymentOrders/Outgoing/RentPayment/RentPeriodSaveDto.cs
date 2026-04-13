using Moedelo.Infrastructure.AspNetCore.Validation;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.RentPayment
{
    /// <summary>
    /// Период платежа
    /// </summary>
    public class RentPeriodSaveDto
    {
        /// <summary>
        /// Идентификатор строки графика платежей из договора аренды
        /// </summary>
        [IdIntValue]
        [RequiredValue]
        public int Id { get; set; }

        /// <summary>
        /// Сумма платежа за этот период
        /// </summary>
        [SumValue]
        [RequiredValue]
        public decimal Sum { get; set; }

    }
}
