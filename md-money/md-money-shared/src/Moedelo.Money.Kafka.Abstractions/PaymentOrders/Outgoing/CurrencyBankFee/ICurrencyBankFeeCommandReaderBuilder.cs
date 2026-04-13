using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee
{
    // note: Должен использоваться только в md-money!
    public interface ICurrencyBankFeeCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        ICurrencyBankFeeCommandReaderBuilder OnImport(Func<ImportCurrencyBankFee, Task> onCommand);
        ICurrencyBankFeeCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateCurrencyBankFee, Task> onCommand);
        ICurrencyBankFeeCommandReaderBuilder OnImportWithMissingExchangeRate(Func<ImportWithMissingExchangeRateCurrencyBankFee, Task> onCommand);
    }
}