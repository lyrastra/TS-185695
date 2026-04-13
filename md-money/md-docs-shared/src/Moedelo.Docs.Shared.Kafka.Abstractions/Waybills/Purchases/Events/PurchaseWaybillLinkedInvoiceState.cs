namespace Moedelo.Docs.Shared.Kafka.Abstractions.Waybills.Purchases.Events
{
    public class PurchaseWaybillLinkedInvoiceState
    {
        /// <summary>
        /// Базовый идентификатор
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Счет-фактура
        /// </summary>
        public PurchaseWaybillNewState.LinkedInvoice Invoice { get; set; }
    }
}