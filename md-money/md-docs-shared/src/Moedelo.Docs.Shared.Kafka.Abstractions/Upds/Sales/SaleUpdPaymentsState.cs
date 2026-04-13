namespace Moedelo.Docs.Shared.Kafka.Abstractions.Upds.Sales
{
    public class SaleUpdPaymentsState
    {
        /// <summary>
        /// Базовый идентификатор
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Платежи
        /// </summary>
        public SaleUpdNewState.LinkedPayment[] Payments { get; set; }
    }
}