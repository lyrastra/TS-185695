using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.LoanObtaining.Commands;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.MediationFee.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.LoanObtaining
{
    // note: Должен использоваться только в md-money!
    public interface ILoanObtainingCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        ILoanObtainingCommandReaderBuilder OnImport(Func<ImportLoanObtaining, Task> onCommand);
        ILoanObtainingCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateLoanObtaining, Task> onCommand);
        ILoanObtainingCommandReaderBuilder OnImportWithMissingContract(Func<ImportWithMissingContractLoanObtaining, Task> onCommand);
        ILoanObtainingCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorLoanObtaining, Task> onCommand);
    }
}