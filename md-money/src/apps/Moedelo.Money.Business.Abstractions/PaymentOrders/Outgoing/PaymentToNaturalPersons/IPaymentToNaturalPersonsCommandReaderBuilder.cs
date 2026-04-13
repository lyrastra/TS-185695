using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public interface IPaymentToNaturalPersonsCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IPaymentToNaturalPersonsCommandReaderBuilder OnImport(Func<ImportPaymentToNaturalPersons, Task> onCommand);
        IPaymentToNaturalPersonsCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicatePaymentToNaturalPersons, Task> onCommand);
        IPaymentToNaturalPersonsCommandReaderBuilder OnImportWithMissingEmployee(Func<ImportWithMissingEmployeePaymentToNaturalPersons, Task> onCommand);
    }
}