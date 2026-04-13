using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Estate.Kafka.Abstractions.Events.EventData.InventoryCard
{
    public class InventoryCardUpdatedMessage : IEntityEventData
    {
        /// <summary>
        /// Идентификатор инвентарной карточки
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер инвентарной карточки
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата инвентарной карточки
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// Наименование ОС
        /// </summary>
        public string FixedAssetName { get; set; }

        /// <summary>
        /// Инвентарный номер
        /// </summary>
        public string InventoryNumber { get; set; }

        /// <summary>
        /// ОКОФ
        /// </summary>
        public string Okof { get; set; }

        /// <summary>
        /// Включить амортизацию
        /// </summary>
        public bool UseAmortization { get; set; }

        /// <summary>
        /// Создано из остатков
        /// </summary>
        public bool IsFromBalances { get; set; }

        /// <summary>
        /// Дата выбытия ОС
        /// </summary>
        public DateTime? DismissalDate { get; set; }

        /// <summary>
        /// Параметры налогового учёта
        /// </summary>
        public InventoryCardTaxDescription TaxDescription { get; set; }

        /// <summary>
        /// Документы, связанные с инвентарной карточкой
        /// </summary>
        public FixedAssetInvestmentDocumentEventData[] FixedAssetInvestmentDocuments { get; set; }
    }
}