using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Cashier;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.Cashier
{
    public interface ICashierApiClient : IDI
    {
        Task<CashierDto> GetAsync(int firmId, int userId, long id);

        Task<CashierCollectionDto> GetCashiersListAsync(int firmId, int userId);
    }
}