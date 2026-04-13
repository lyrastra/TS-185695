using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson.Commands;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;

public interface IRefundFromAccountablePersonCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
{
    IRefundFromAccountablePersonCommandReaderBuilder OnImport(Func<ImportRefundFromAccountablePerson, Task> onCommand);
    IRefundFromAccountablePersonCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateRefundFromAccountablePerson, Task> onCommand);
    IRefundFromAccountablePersonCommandReaderBuilder OnImportWithMissingEmployee(Func<ImportWithMissingEmployeeRefundFromAccountablePerson, Task> onCommand);
}