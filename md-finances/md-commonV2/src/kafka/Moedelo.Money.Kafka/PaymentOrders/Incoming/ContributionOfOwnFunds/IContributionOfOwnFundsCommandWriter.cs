using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Incoming.ContributionOfOwnFunds.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.ContributionOfOwnFunds
{
    public interface IContributionOfOwnFundsCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportContributionOfOwnFunds commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateContributionOfOwnFunds commandData);
    }
}