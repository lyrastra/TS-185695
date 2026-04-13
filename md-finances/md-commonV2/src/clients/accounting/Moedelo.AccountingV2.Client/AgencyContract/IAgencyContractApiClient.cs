using Moedelo.AccountingV2.Client.AgencyContract.Dto;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.AgencyContract
{
    public interface IAgencyContractApiClient : IDI
    {
        Task<long> CreateOrUpdateAsync(int firmId, int userId, AgencyContractDto dto);
        Task<AgencyContractDto> GetOrderByBaseIdAsync(int firmId, int userId, long id);
        Task DeleteAsync(int firmId, int userId, long id);
    }
}

