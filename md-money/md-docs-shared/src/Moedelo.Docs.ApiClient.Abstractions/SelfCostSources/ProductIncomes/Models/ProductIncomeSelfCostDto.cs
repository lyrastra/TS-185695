using System;
using System.Collections.Generic;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.ProductIncomes.Models
{
    /// <summary>
    /// Представляет приход без документов с позициями номенклатур для расчёта себестоимости.
    /// </summary>
    public class ProductIncomeSelfCostDto
    {
        /// <summary>
        /// Идентификатор прихода без документов.
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер документа.
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата документа.
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// Список позиций прихода без документов.
        /// </summary>
        public IReadOnlyCollection<ProductIncomeItemSelfCostDto> Items { get; set; }
    }
}