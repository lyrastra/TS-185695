using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment.Events.Models;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment.Events
{
    public class UnifiedBudgetaryPaymentUpdated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public long CashId { get; set; }

        public decimal Sum { get; set; }

        public IReadOnlyCollection<UnifiedBudgetarySubPayment> SubPayments { get; set; }

        /// <summary>
        /// Получатель платежа
        /// </summary>
        public string Recipient { get; set; }

        public string Destination { get; set; }

        /// <summary>
        /// Тип кассового ордера, который был до сохранения (если была смена типа)
        /// </summary>
        public OperationType OldOperationType { get; set; }

        /// <summary>
        /// Список базовых идентификаторов дочерних документов,
        /// которые были удалены при обновлении родительской операции
        /// note: Вероятно лучше сделать это отдельными событиями
        /// </summary>
        public IReadOnlyCollection<long> DeletedSubPaymentDocumentIds { get; set; }
    }
}
