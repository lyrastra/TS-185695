using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto.Dto;
using Moedelo.AccPostings.Enums;
using Moedelo.Common.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto
{
    public interface ISubcontoClient
    {
        Task<List<SubcontoDto>> GetByIdsAsync(
            FirmId firmId, UserId userId, IReadOnlyCollection<long> ids, bool isFromReadonlyDb = false);

        Task<SubcontoDto> GetOrCreateTextSubcontoAsync(FirmId firmId, UserId userId, SubcontoType type, string name);

        Task<List<CostItemGroupDto>> GetCostItemGroupsAsync(FirmId firmId, UserId userId);

        Task<SubcontoDto> GetDefaultSubcontoAsync(FirmId firmId, UserId userId, SubcontoType type);

        Task<List<NomenclatureGroupDto>> GetNomenclatureGroupsAsync(FirmId firmId, UserId userId);

        Task<List<NdsRateDto>> GetNdsRatesAsync(FirmId firmId, UserId userId);

        Task<SubcontoDto[]> GetByTypeAutocompleteAsync(FirmId firmId, UserId userId, SubcontoType type, string query = "", int count = 5);

        Task<long> SaveAsync(int firmId, int userId, SubcontoDto subconto);

        Task DeleteAsync(int firmId, int userId, long id);
    }
}
