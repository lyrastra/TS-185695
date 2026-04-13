using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.KontragentsV2.Dto;

namespace Moedelo.KontragentsV2.Client.SettlementAccounts
{
    public interface IKontragentSettlementAccountsClient : IDI
    {
        Task<KontragentSettlementAccountDto> GetByIdAsync(int firmId, int userId, long id);

        Task<List<KontragentSettlementAccountDto>> GetByKontragentAsync(int firmId, int userId, int kontragentId);

        Task<List<KontragentSettlementAccountDto>> GetByNumberAsync(int firmId, int userId, int kontragentId, string number);

        Task<long> SaveAsync(int firmId, int userId, KontragentSettlementAccountDto settlementAccount);
        Task UpdateLastChoiceDateAsync(int firmId, int userId, long settlementAccountId);
        Task AddAllAsync(int firmId, int userId, List<KontragentSettlementAccountDto> dtos);
    }
}