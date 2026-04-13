using System;
using System.Collections.Generic;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.Upds.Models.Purchases
{
    /// <summary>
    /// Представляет УПД покупки с позициями номенклатур для расчёта себестоимости.
    /// </summary>
    public sealed class PurchaseUpdSelfCostDto
    {
        /// <summary>
        /// Идентификатор УПД.
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер УПД.
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата УПД.
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// Список позиций УПД.
        /// </summary>
        public IReadOnlyList<PurchaseUpdItemSelfCostDto> Items { get; set; }

        /// <summary>
        /// Сумма документа
        /// </summary>
        public decimal DocumentSum { get; set; }
    }
}