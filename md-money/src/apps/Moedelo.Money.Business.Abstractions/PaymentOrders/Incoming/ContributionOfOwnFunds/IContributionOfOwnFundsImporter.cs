using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionOfOwnFunds;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds
{
    public interface IContributionOfOwnFundsImporter
    {
        Task ImportAsync(ContributionOfOwnFundsImportRequest request);
    }
}