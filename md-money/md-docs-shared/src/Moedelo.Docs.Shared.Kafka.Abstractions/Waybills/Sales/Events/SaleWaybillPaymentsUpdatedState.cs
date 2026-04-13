namespace Moedelo.Docs.Shared.Kafka.Abstractions.Waybills.Sales.Events
{
    public class SaleWaybillPaymentsUpdatedState
    {
        /// <summary>
        /// Базовый идентификатор
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Платежи
        /// </summary>
        public SaleWaybillNewState.LinkedPayment[] Payments { get; set; }
    }
}