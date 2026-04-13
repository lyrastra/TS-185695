using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencySale.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencySale
{
    public interface IOutgoingCurrencySaleCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IOutgoingCurrencySaleCommandReaderBuilder OnImport(Func<ImportOutgoingCurrencySale, Task> onCommand);
        IOutgoingCurrencySaleCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateOutgoingCurrencySale, Task> onCommand);
        IOutgoingCurrencySaleCommandReaderBuilder OnImportWithMissingExchangeRate(Func<ImportWithMissingMissingExchangeRateOutgoingCurrencySale, Task> onCommand);
    }
}