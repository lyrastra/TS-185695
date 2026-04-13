namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.RentPayment
{
    public class RentPeriodResponseDto
    {
        /// <summary>
        /// Идентификатор строки графика платежей из договора аренды
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Сумма платежа за этот период
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Описание периода
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Тип платежа в периоде: ежемесячный или выкуп
        /// </summary>
        public RentPeriodType PaymentType { get; set; }

        /// <summary>
        /// Сумма которую требуется оплатить - остаток оплаты
        /// </summary>
        public decimal? PaymentRequiredSum { get; set; }
    }
}
