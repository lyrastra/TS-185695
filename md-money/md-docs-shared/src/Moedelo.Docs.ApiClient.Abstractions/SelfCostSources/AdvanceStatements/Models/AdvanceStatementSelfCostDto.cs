using System;
using System.Collections.Generic;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.AdvanceStatements.Models
{
    /// <summary>
    /// Представляет АО с позициями номенклатур для расчёта себестоимости.
    /// </summary>
    public sealed class AdvanceStatementSelfCostDto
    {
        /// <summary>
        /// Идентификатор АО.
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер АО.
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата АО.
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// Сумма АО.
        /// </summary>
        public decimal DocumentSum { get; set; }

        /// <summary>
        /// Список позиций АО.
        /// </summary>
        public IReadOnlyCollection<AdvanceStatementItemSelfCostDto> Items { get; set; }

        /// <summary>
        /// Учитывается в СНО
        /// </summary>
        public TaxationSystemType? TaxationSystem { get; set; }
    }
}