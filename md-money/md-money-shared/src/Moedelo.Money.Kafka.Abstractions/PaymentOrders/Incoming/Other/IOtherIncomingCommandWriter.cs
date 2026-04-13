using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.Other.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.Other
{
    public interface IOtherIncomingCommandWriter
    {
        /// <summary>
        /// Импорт в раздел "Деньги" (в т. ч. в "красную таблицу" в недозаполненном виде)
        /// </summary>
        Task WriteImportAsync(ImportOtherIncoming commandData);

        Task WriteImportDuplicateAsync(ImportOtherIncomingDuplicate commandData);
    }
}
