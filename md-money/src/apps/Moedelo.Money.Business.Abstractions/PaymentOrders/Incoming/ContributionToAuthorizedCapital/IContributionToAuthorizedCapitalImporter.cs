using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionToAuthorizedCapital
{
    public interface IContributionToAuthorizedCapitalImporter
    {
        Task ImportAsync(ContributionToAuthorizedCapitalImportRequest request);
    }
}