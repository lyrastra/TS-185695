using System.Threading.Tasks;
using Moedelo.AccountingV2.Client.RetailRevenue.Dto;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.RetailRevenue
{
    public interface IRetailRevenueApiClient : IDI
    {
        Task<RetailRevenueDto> GetByBaseIdAsync(int firmId, int userId, long cashierId, long id);

        Task<RetailRevenueDto> GetAsync(int firmId, int userId, long cashierId, long id);

        Task<RetailRevenueDto> CreateAsync(int firmId, int userId, long cashierId, RetailRevenueDto dto);

        Task<RetailRevenueCollectionDto> GetListAsync(int firmId, int userId, CashierPaginationCriterions cashierPaginationRequest);

        Task<bool> ExistsCashOrderAsync(int firmId, int userId, long cashierId, long baseId);
        
        Task<RetailRevenueDto> UpdateAsync(int firmId, int userId, long cashierId, RetailRevenueDto dto);
        
        Task DeleteAsync(int firmId, int userId, long cashierId, long baseId);

    }
}