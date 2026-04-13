using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.LoanRepayment.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanRepayment
{
    public interface ILoanRepaymentCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        ILoanRepaymentCommandReaderBuilder OnImport(Func<ImportLoanRepayment, Task> onCommand);
        ILoanRepaymentCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateLoanRepayment, Task> onCommand);
        ILoanRepaymentCommandReaderBuilder OnImportWithMissingContract(Func<ImportWithMissingContractLoanRepayment, Task> onCommand);
        ILoanRepaymentCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorLoanRepayment, Task> onCommand);
    }
}