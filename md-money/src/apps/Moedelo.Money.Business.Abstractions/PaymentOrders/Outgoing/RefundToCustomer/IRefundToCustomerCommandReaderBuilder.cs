using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RefundToCustomer.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RefundToCustomer
{
    public interface IRefundToCustomerCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IRefundToCustomerCommandReaderBuilder OnImport(Func<ImportRefundToCustomer, Task> onCommand);
        IRefundToCustomerCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateRefundToCustomer, Task> onCommand);
        IRefundToCustomerCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorRefundToCustomer, Task> onCommand);
    }
}