namespace Moedelo.Docs.Shared.Kafka.Abstractions.Waybills.Sales.Events
{
    public class SaleWaybillLinkedBillState
    {
        /// <summary>
        /// Базовый идентификатор
        /// </summary>
        public long DocumentBaseId { get; set; }
        
        /// <summary>
        /// по счету
        /// </summary>
        public SaleWaybillNewState.LinkedBill Bill { get; set; }
    }
}