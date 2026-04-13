using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount
{
    // note: Должен использоваться только в md-money!
    public interface ICurrencyTransferToAccountCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        ICurrencyTransferToAccountCommandReaderBuilder OnImport(Func<ImportCurrencyTransferToAccount, Task> onCommand);
        ICurrencyTransferToAccountCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateCurrencyTransferToAccount, Task> onCommand);
        ICurrencyTransferToAccountCommandReaderBuilder OnImportWithMissingCurrencySettlementAccount(Func<ImportWithMissingCurrencySettlementAccountCurrencyTransferToAccount, Task> onCommand);
    }
}