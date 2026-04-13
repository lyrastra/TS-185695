using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase
{
    public interface IOutgoingCurrencyPurchaseCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IOutgoingCurrencyPurchaseCommandReaderBuilder OnImport(Func<ImportOutgoingCurrencyPurchase, Task> onCommand);
        IOutgoingCurrencyPurchaseCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateOutgoingCurrencyPurchase, Task> onCommand);
        IOutgoingCurrencyPurchaseCommandReaderBuilder OnImportWithMissingExchangeRate(Func<ImportWithMissingMissingExchangeRateOutgoingCurrencyPurchase, Task> onCommand);
        IOutgoingCurrencyPurchaseCommandReaderBuilder OnImportWithMissingCurrencySettlementAccount(Func<ImportWithMissingCurrencySettlementAccountOutgoingCurrencyPurchase, Task> onCommand);
    }
}