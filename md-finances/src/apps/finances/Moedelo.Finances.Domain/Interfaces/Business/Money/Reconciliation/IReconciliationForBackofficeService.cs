using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Models.Money.Reconciliation;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money.Reconciliation
{
    public interface IReconciliationForBackofficeService : IDI
    {
        Task ProcessAsync(IUserContext userContext, ReconciliationForBackofficeRequest request);
    }
}
