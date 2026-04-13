using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer
{
    public interface IPaymentFromCustomerCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IPaymentFromCustomerCommandReaderBuilder OnImport(Func<ImportPaymentFromCustomer, Task> onCommand);
        IPaymentFromCustomerCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicatePaymentFromCustomer, Task> onCommand);
        IPaymentFromCustomerCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorPaymentFromCustomer, Task> onCommand);
    }
}
