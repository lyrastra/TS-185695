using Moedelo.Docs.Enums;

namespace Moedelo.Docs.Shared.Kafka.Abstractions.Waybills.Sales.Events
{
    public class SaleWaybillSignStatusState
    {
        /// <summary>
        /// Базовый идентификатор
        /// </summary>
        public long DocumentBaseId { get; set; }
        
        /// <summary>
        /// Статус документа (Подписан/Скан/Нет)
        /// </summary>
        public SignStatus SignStatus { get; set; }
    }
}