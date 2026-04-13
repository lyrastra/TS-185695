using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Incoming.ContributionToAuthorizedCapital.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.ContributionToAuthorizedCapital
{
    public interface IContributionToAuthorizedCapitalCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportContributionToAuthorizedCapital commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateContributionToAuthorizedCapital commandData);
        Task WriteImportWithMissingContractorAsync(string key, string token, ImportWithMissingContractorContributionToAuthorizedCapital commandData);
    }
}