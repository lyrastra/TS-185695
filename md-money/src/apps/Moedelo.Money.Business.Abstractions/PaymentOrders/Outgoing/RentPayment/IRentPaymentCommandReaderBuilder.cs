using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RentPayment.Commands;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RentPayment;

public interface IRentPaymentCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
{
    IRentPaymentCommandReaderBuilder OnImport(Func<ImportRentPayment, Task> onCommand);
    IRentPaymentCommandReaderBuilder OnImportWithMissingContract(Func<ImportWithMissingContractRentPayment, Task> onCommand);
    IRentPaymentCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorRentPayment, Task> onCommand);
    IRentPaymentCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateRentPayment, Task> onCommand);
}