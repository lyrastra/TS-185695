using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount
{
    // note: Должен использоваться только в md-money!
    public interface ICurrencyTransferFromAccountCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        ICurrencyTransferFromAccountCommandReaderBuilder OnImport(Func<ImportCurrencyTransferFromAccount, Task> onCommand);
        ICurrencyTransferFromAccountCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateCurrencyTransferFromAccount, Task> onCommand);
        ICurrencyTransferFromAccountCommandReaderBuilder OnImportWithMissingCurrencySettlementAccount(Func<ImportWithMissingCurrencySettlementAccountCurrencyTransferFromAccount, Task> onCommand);
    }
}