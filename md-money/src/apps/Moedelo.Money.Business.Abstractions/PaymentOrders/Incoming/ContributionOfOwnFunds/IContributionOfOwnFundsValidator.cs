using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionOfOwnFunds;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds
{
    public interface IContributionOfOwnFundsValidator
    {
        Task ValidateAsync(ContributionOfOwnFundsSaveRequest request);
    }
}