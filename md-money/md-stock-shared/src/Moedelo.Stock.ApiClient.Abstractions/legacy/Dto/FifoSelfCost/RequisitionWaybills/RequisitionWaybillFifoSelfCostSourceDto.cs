using System;
using System.Collections.Generic;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.FifoSelfCost
{

    /// <summary>
    /// Представляет требование-накладную с позициями номенклатур для расчёта себестоимости.
    /// </summary>
    public sealed class RequisitionWaybillFifoSelfCostSourceDto
    {
        /// <summary>
        /// Идентификатор требования-накладной.
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер ОРП.
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата требования-накладной.
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// Список позиций требования-накладной.
        /// </summary>
        public IReadOnlyCollection<RequisitionWaybillItemSelfCostDto> Items { get; set; }
    }
}
