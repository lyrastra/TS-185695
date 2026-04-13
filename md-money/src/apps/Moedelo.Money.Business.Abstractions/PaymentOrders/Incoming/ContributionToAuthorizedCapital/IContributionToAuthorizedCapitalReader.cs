using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionToAuthorizedCapital
{
    public interface IContributionToAuthorizedCapitalReader
    {
        Task<ContributionToAuthorizedCapitalResponse> GetByBaseIdAsync(long baseId);
    }
}
