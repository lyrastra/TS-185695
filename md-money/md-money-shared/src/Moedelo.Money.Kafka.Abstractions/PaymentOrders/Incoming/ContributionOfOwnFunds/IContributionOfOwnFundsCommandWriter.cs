using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds
{
    public interface IContributionOfOwnFundsCommandWriter
    {
        Task WriteImportAsync(ImportContributionOfOwnFunds commandData);
        
        Task WriteImportDuplicateAsync(ImportDuplicateContributionOfOwnFunds commandData);

        Task WriteImportAmbiguousOperationTypeAsync(ImportAmbiguousOperationTypeContributionOfOwnFunds commandData);
    }
}