using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.RequisitesV2.Client.TaxAmnesty
{
    public interface ITaxAmnestyClient : IDI
    {
        Task ActivateIfAvailableAsync(int firmId, int userId);
        Task<bool> GetAsync(int firmId, int userId);
    }
}
