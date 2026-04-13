using System.Threading.Tasks;
using Moedelo.CashV2.Dto.LifePay;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CashV2.Client.Contracts
{
    public interface ILifePayApiClient : IDI
    {
        Task<bool> RegisterLifePayUserAsync(int firmId, int userId, LifePayIntegrationClientData registerLifePayUserClientData);
    }
}