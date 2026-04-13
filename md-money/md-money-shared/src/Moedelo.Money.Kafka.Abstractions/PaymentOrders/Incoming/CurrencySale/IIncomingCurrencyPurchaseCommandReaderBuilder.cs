using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencySale.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencySale
{
    // note: Должен использоваться только в md-money!
    public interface IIncomingCurrencySaleCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IIncomingCurrencySaleCommandReaderBuilder OnImport(Func<ImportIncomingCurrencySale, Task> onCommand);
        IIncomingCurrencySaleCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateIncomingCurrencySale, Task> onCommand);
        IIncomingCurrencySaleCommandReaderBuilder OnImportWithMissingCurrencySettlementAccount(Func<ImportWithMissingCurrencySettlementAccountIncomingCurrencySale, Task> onCommand);
    }
}