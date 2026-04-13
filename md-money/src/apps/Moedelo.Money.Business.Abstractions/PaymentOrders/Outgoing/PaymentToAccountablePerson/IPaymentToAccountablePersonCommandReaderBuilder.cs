using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    public interface IPaymentToAccountablePersonCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IPaymentToAccountablePersonCommandReaderBuilder OnImport(Func<ImportPaymentToAccountablePerson, Task> onCommand);
        IPaymentToAccountablePersonCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicatePaymentToAccountablePerson, Task> onCommand);
        IPaymentToAccountablePersonCommandReaderBuilder OnImportWithMissingEmployee(Func<ImportWithMissingEmployeePaymentToAccountablePerson, Task> onCommand);
        IPaymentToAccountablePersonCommandReaderBuilder OnApplyIgnoreNumber(Func<ApplyIgnoreNumberPaymentToAccountablePerson, Task> onCommand);
    }
}