namespace Moedelo.Docs.Shared.Kafka.Abstractions.Upds.Sales
{
    public class SaleUpdStampAndSignState
    {
        /// <summary>
        /// Базовый идентификатор
        /// </summary>
        public long DocumentBaseId { get; set; }
        
        /// <summary>
        /// Состояние переключетеля "Печать и подпись"
        /// </summary>
        public bool UseStampAndSign { get; set; }
    }
}