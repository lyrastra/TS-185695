using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Other.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Other
{
    public interface IOtherOutgoingCommandWriter
    {
        /// <summary>
        /// Импорт в раздел "Деньги" (в т. ч. в "красную таблицу" в недозаполненном виде)
        /// </summary>
        Task WriteImportAsync(ImportOtherOutgoing commandData);

        Task WriteImportDuplicateAsync(ImportOtherOutgoingDuplicate commandData);
    }
}
