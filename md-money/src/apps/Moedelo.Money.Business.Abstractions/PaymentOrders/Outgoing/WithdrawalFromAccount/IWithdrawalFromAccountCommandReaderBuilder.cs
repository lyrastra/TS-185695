using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount
{
    public interface IWithdrawalFromAccountCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IWithdrawalFromAccountCommandReaderBuilder OnImport(Func<ImportWithdrawalFromAccount, Task> onCommand);
        IWithdrawalFromAccountCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateWithdrawalFromAccount, Task> onCommand);
        IWithdrawalFromAccountCommandReaderBuilder OnApplyIgnoreNumber(Func<ApplyIgnoreNumberWithdrawalFromAccount, Task> onCommand);
    }
}
