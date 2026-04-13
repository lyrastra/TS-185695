using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.ContributionToAuthorizedCapital.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.ContributionToAuthorizedCapital
{
    // note: Должен использоваться только в md-money!
    public interface IContributionToAuthorizedCapitalCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IContributionToAuthorizedCapitalCommandReaderBuilder OnImport(Func<ImportContributionToAuthorizedCapital, Task> onCommand);
        IContributionToAuthorizedCapitalCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateContributionToAuthorizedCapital, Task> onCommand);
        IContributionToAuthorizedCapitalCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorContributionToAuthorizedCapital, Task> onCommand);
    }
}