using Moedelo.Docs.Enums;

namespace Moedelo.Docs.Shared.Kafka.Abstractions.Upds.Sales
{
    public class SaleUpdSignStatusState
    {
        /// <summary>
        /// Базовый идентификатор
        /// </summary>
        public long DocumentBaseId { get; set; }
        
        /// <summary>
        /// Состояние переключетеля "Подписан"
        /// </summary>
        public SignStatus SignStatus { get; set; }
    }
}