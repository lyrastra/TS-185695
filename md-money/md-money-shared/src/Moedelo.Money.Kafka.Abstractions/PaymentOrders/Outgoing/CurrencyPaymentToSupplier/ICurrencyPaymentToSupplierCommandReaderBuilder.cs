using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    // note: Должен использоваться только в md-money!
    public interface ICurrencyPaymentToSupplierCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        ICurrencyPaymentToSupplierCommandReaderBuilder OnImport(Func<ImportCurrencyPaymentToSupplier, Task> onCommand);
        ICurrencyPaymentToSupplierCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateCurrencyPaymentToSupplier, Task> onCommand);
        ICurrencyPaymentToSupplierCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingMissingContractorCurrencyPaymentToSupplier, Task> onCommand);
        ICurrencyPaymentToSupplierCommandReaderBuilder OnImportWithMissingExchangeRate(Func<ImportWithMissingMissingExchangeRateCurrencyPaymentToSupplier, Task> onCommand);
    }
}