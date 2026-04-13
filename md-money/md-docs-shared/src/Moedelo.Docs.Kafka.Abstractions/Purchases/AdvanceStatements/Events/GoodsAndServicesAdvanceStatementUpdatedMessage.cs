using System.Collections.Generic;
using Moedelo.Docs.Enums;
using Moedelo.Docs.Kafka.Abstractions.Purchases.AdvanceStatements.Models;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.AdvanceStatements.Events
{
    public class GoodsAndServicesAdvanceStatementUpdatedMessage : BaseAdvanceStatementMessage
    {
        public IReadOnlyCollection<GoodsAndServicesItem> Items { get; set; }

        /// <summary>
        /// Тип авансового отчёта, который был до сохранения
        /// </summary>
        public AdvanceStatementType OldType { get; set; }
    }
}