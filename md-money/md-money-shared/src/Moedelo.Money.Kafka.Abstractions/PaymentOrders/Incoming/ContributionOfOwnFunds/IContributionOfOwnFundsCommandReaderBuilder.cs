using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds
{
    // note: Должен использоваться только в md-money!
    public interface IContributionOfOwnFundsCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IContributionOfOwnFundsCommandReaderBuilder OnImport(Func<ImportContributionOfOwnFunds, Task> onCommand);
        IContributionOfOwnFundsCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateContributionOfOwnFunds, Task> onCommand);
        IContributionOfOwnFundsCommandReaderBuilder OnImportAmbiguousOperationType(Func<ImportAmbiguousOperationTypeContributionOfOwnFunds, Task> onCommand);
    }
}