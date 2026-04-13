using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionToAuthorizedCapital
{
    public interface IContributionToAuthorizedCapitalCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(ContributionToAuthorizedCapitalSaveRequest request);
    }
}
