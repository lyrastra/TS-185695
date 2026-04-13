using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using System.Collections.Generic;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Providing.Commands
{
    public class PaymentOrdersBatchProvideCommand : IEntityCommandData
    {
        /// <summary>
        /// Идентификатор для корректной работы очереди проведения
        /// </summary>
        public long ProvidingStateId { get; set; }

        /// <summary>
        /// Идентификаторы документов
        /// </summary>
        public IReadOnlyCollection<long> DocumentBaseIds { get; set; }
    }
}
