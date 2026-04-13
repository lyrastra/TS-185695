using System.Collections.Generic;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.AccountingStatements.Kafka.Abstractions.SelfCostTax.Events
{
    /// <summary>
    /// Событие по удалению бухсправок с типом "Комиссия за эквайринг"
    /// </summary>
    public class SelfCostTaxMultipleDeleted : IEntityEventData
    {
        /// <summary>
        /// BaseId удаляемых бух. справок
        /// </summary>
        public IReadOnlyCollection<long> DocumentBaseIds { get; set; }
    }
}