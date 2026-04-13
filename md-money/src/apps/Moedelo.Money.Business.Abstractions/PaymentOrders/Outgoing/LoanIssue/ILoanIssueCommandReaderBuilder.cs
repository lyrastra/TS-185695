using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.LoanIssue.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanIssue
{
    public interface ILoanIssueCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        ILoanIssueCommandReaderBuilder OnImport(Func<ImportLoanIssue, Task> onCommand);
        ILoanIssueCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateLoanIssue, Task> onCommand);
        ILoanIssueCommandReaderBuilder OnImportWithMissingContract(Func<ImportWithMissingContractLoanIssue, Task> onCommand);
        ILoanIssueCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorLoanIssue, Task> onCommand);
    }
}
