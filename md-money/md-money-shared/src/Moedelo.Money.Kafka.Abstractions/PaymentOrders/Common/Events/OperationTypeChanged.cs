using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Common.Events
{
    /// <summary>
    /// При смене типа операции п/п пересоздается с новым ИД
    /// </summary>
    public sealed class OperationTypeChanged : IEntityEventData
    {
        /// <summary>
        /// Предыдущий идентификатор документа
        /// </summary>
        public long OldDocumentBaseId { get; set; }

        /// <summary>
        /// Предыдущий тип операции
        /// </summary>
        public OperationType OldOperationType { get; set; }

        /// <summary>
        /// Новый идентификатор документа
        /// </summary>
        public long NewDocumentBaseId { get; set; }

        /// <summary>
        /// Новый тип операции
        /// </summary>
        public OperationType NewOperationType { get; set; }
    }
}
