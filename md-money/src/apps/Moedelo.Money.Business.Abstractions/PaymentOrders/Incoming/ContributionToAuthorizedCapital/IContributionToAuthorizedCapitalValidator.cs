using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionToAuthorizedCapital
{
    public interface IContributionToAuthorizedCapitalValidator
    {
        Task ValidateAsync(ContributionToAuthorizedCapitalSaveRequest request);
    }
}
