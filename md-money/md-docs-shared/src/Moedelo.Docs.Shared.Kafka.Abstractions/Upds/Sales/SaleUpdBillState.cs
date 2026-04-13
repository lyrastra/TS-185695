namespace Moedelo.Docs.Shared.Kafka.Abstractions.Upds.Sales
{
    public class SaleUpdBillState
    {
        /// <summary>
        /// Базовый идентификатор
        /// </summary>
        public long DocumentBaseId { get; set; }
        
        /// <summary>
        /// по счету
        /// </summary>
        public SaleUpdNewState.LinkedBill Bill { get; set; }
    }
}