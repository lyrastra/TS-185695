using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Deduction.Commands;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Deduction
{
    public interface IOutgoingPaymentOrderDeductionCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IOutgoingPaymentOrderDeductionCommandReaderBuilder OnImport(Func<ImportDeduction, Task> onCommand);
        IOutgoingPaymentOrderDeductionCommandReaderBuilder OnImportDuplicate(Func<ImportDeductionDuplicate, Task> onCommand);
        IOutgoingPaymentOrderDeductionCommandReaderBuilder OnImportWithMissingContractor(Func<ImportDeductionWithMissingContractor, Task> onCommand);
        IOutgoingPaymentOrderDeductionCommandReaderBuilder OnImportWithMissingEmployee(Func<ImportDeductionWithMissingEmployee, Task> onCommand);
    }
}
