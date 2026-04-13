using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionOfOwnFunds;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds
{
    public interface IContributionOfOwnFundsReader
    {
        Task<ContributionOfOwnFundsResponse> GetByBaseIdAsync(long baseId);
    }
}
