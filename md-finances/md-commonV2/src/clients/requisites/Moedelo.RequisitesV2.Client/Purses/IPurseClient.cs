using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Dto.Purses;

namespace Moedelo.RequisitesV2.Client.Purses
{
    public interface IPurseClient : IDI
    {
        Task<List<PurseDto>> GetAsync(int firmId, int userId);

        Task<List<PurseDto>> GetByIdsAsync(int firmId, int userId, IReadOnlyCollection<int> ids);

        Task<PurseDto> GetByNameAsync(int firmId, int userId, string name);

        Task UpdateNameAsync(int firmId, int userId, int purseId, string name);

        Task<int> SaveVirtualPurseAsync(int firmId, int userId, PurseDto purse);

        Task<List<int>> CanBeDeletedAsync(int firmId, int userId, IReadOnlyCollection<int> ids);

        Task DeleteByIdsAsync(int firmId, int userId, IReadOnlyCollection<int> ids, bool removeRelatedKontragents = false);

        Task<List<BankSettlementPurseDto>> GetByRecipientSettlementsAsync(int firmId, int userId, IReadOnlyCollection<string> numbers);

        Task<int> SaveBankSettlementPurseAsync(int firmId, int userId, BankSettlementPurseDto purse);
    }
}
