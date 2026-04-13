using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.LoanReturn.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanReturn
{
    public interface ILoanReturnCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        ILoanReturnCommandReaderBuilder OnImport(Func<ImportLoanReturn, Task> onCommand);
        ILoanReturnCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateLoanReturn, Task> onCommand);
        ILoanReturnCommandReaderBuilder OnImportWithMissingContract(Func<ImportWithMissingContractLoanReturn, Task> onCommand);
        ILoanReturnCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorLoanReturn, Task> onCommand);
    }
}