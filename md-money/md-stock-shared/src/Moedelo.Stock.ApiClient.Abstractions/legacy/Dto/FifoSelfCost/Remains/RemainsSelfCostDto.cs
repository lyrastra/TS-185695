using System;
using System.Collections.Generic;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.FifoSelfCost
{
    /// <summary>
    /// Модель остатков для расчёта себестоимости в ФИФО
    /// </summary>
    public class RemainsSelfCostDto
    {
        /// <summary>
        /// Идентификатор складской операции с типом "Ввод остатков".
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Дата ввода остатков.
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// Список позиций требования-накладной.
        /// </summary>
        public IReadOnlyCollection<RemainsItemSelfCostDto> Items { get; set; }
    }
}