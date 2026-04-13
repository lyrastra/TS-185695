using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.ContributionToAuthorizedCapital.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.ContributionToAuthorizedCapital
{
    public interface IContributionToAuthorizedCapitalCommandWriter
    {
        Task WriteImportAsync(
            ImportContributionToAuthorizedCapital commandData);

        Task WriteImportDuplicateAsync(
            ImportDuplicateContributionToAuthorizedCapital commandData);

        Task WriteImportWithMissingContractorAsync(
            ImportWithMissingContractorContributionToAuthorizedCapital commandData);
    }
}