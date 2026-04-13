using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyPurchase.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyPurchase
{
    // note: Должен использоваться только в md-money!
    public interface IIncomingCurrencyPurchaseCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IIncomingCurrencyPurchaseCommandReaderBuilder OnImport(Func<ImportIncomingCurrencyPurchase, Task> onCommand);
        IIncomingCurrencyPurchaseCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateIncomingCurrencyPurchase, Task> onCommand);
    }
}