using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.AdvanceStatements.Events
{
    /// <summary>
    /// Авансовый отчет: удаление
    /// </summary>
    public class AdvanceStatementDeletedMessage : IEntityEventData
    {
        public long DocumentBaseId { get; set; }
    }
}
