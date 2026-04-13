using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit
{
    public interface IWithdrawalOfProfitCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IWithdrawalOfProfitCommandReaderBuilder OnImport(Func<ImportWithdrawalOfProfit, Task> onCommand);
        IWithdrawalOfProfitCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateWithdrawalOfProfit, Task> onCommand);
        IWithdrawalOfProfitCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorWithdrawalOfProfit, Task> onCommand);
        IWithdrawalOfProfitCommandReaderBuilder OnApplyIgnoreNumber(Func<ApplyIgnoreNumberWithdrawalOfProfit, Task> onCommand);

    }
}
