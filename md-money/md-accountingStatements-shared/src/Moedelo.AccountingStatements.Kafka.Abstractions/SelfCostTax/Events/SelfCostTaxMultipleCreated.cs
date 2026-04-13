using System.Collections.Generic;
using Moedelo.AccountingStatements.Kafka.Abstractions.SelfCostTax.Models;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.AccountingStatements.Kafka.Abstractions.SelfCostTax.Events
{
    /// <summary>
    /// Событие по созданию бухсправок "Себестоимость НУ (ФИФО)"
    /// </summary>
    public class SelfCostTaxMultipleCreated : IEntityEventData
    {
        public IReadOnlyCollection<SelfCostTaxCreatedModel> AccountingStatements { get; set; }
    }
}