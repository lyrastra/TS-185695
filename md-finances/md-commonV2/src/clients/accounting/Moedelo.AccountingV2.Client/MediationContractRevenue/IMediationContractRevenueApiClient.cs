using Moedelo.AccountingV2.Client.MediationContractRevenue.Dto;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.MediationContractRevenue
{
    public interface IMediationContractRevenueApiClient : IDI
    {
        Task<long> CreateOrUpdateAsync(int firmId, int userId, MediationContractRevenueDto dto);
        Task<MediationContractRevenueDto> GetOrderByBaseIdAsync(int firmId, int userId, long id);
        Task DeleteAsync(int firmId, int userId, long id);
    }
}
