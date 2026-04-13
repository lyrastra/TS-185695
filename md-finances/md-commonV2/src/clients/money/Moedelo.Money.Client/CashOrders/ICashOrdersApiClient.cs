using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.Money.Client.CashOrders
{
    public interface ICashOrdersApiClient : IDI
    {
        Task<long> GetIdByBaseIdAsync(int firmId, int userId, long documentBaseId);
    }
}
