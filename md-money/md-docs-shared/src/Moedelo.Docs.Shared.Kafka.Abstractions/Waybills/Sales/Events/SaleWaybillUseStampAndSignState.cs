namespace Moedelo.Docs.Shared.Kafka.Abstractions.Waybills.Sales.Events
{
    public class SaleWaybillUseStampAndSignState
    {
        /// <summary>
        /// Базовый идентификатор
        /// </summary>
        public long DocumentBaseId { get; set; }
        
        /// <summary>
        /// Включен флаг "Печать и подпись"
        /// </summary>
        public bool UseStampAndSign { get; set; }
    }
}