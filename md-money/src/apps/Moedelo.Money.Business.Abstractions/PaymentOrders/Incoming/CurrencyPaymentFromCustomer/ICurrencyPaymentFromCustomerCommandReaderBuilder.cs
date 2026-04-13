using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    public interface ICurrencyPaymentFromCustomerCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        ICurrencyPaymentFromCustomerCommandReaderBuilder OnImport(Func<ImportCurrencyPaymentFromCustomer, Task> onCommand);
        ICurrencyPaymentFromCustomerCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateCurrencyPaymentFromCustomer, Task> onCommand);
        ICurrencyPaymentFromCustomerCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorCurrencyPaymentFromCustomer, Task> onCommand);
        ICurrencyPaymentFromCustomerCommandReaderBuilder OnImportWithMissingExchangeRate(Func<ImportWithMissingExchangeRateCurrencyPaymentFromCustomer, Task> onCommand);
    }
}