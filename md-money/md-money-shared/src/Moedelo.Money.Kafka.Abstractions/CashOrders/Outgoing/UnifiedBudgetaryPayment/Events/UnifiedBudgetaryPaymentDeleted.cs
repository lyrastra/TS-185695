using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment.Events
{
    public class UnifiedBudgetaryPaymentDeleted : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        /// <summary>
        /// Список базовых идентификаторов дочерних документов,
        /// которые были удалены при удалении родительской операции
        /// note: Вероятно лучше сделать это отдельными событиями
        /// </summary>
        public IReadOnlyCollection<long> DeletedSubPaymentDocumentIds { get; set; }
    }
}
