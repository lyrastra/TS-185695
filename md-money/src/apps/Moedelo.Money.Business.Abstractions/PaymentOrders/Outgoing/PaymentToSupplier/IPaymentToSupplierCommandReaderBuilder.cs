using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier
{
    public interface IPaymentToSupplierCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IPaymentToSupplierCommandReaderBuilder OnImport(Func<ImportPaymentToSupplier, Task> onCommand);
        IPaymentToSupplierCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicatePaymentToSupplier, Task> onCommand);
        IPaymentToSupplierCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorPaymentToSupplier, Task> onCommand);
        IPaymentToSupplierCommandReaderBuilder OnApplyIgnoreNumber(Func<ApplyIgnoreNumberPaymentToSupplier, Task> onCommand);
    }
}
