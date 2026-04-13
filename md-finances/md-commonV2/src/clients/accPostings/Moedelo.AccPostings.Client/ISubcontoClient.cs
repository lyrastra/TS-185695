using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccPostings.Dto;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.AccPostings.Client
{
    public interface ISubcontoClient : IDI
    {
        Task<List<SubcontoDto>> GetAsync(int firmId, int userId, IReadOnlyCollection<long> subcontoIds);

        Task<List<SubcontoDto>> GetByTypeAsync(int firmId, int userId, SubcontoType type);

        Task<long> SaveAsync(int firmId, int userId, SubcontoSaveRequestDto subconto);

        /// <summary>
        /// Возвращает значение субконто указанного типа, общее для всех фирм
        /// </summary>
        /// <returns>может вернуть null</returns>
        Task<SubcontoDto> GetDefaultSubcontoAsync(int firmId, int userId, SubcontoType type);

        Task<List<NomenclatureGroupDto>> GetNomenclatureGroupsAsync(int firmId, int userId);

        Task<List<CostItemGroupDto>> GetCostItemGroupsAsync(int firmId, int userId);

        Task<List<NdsRateDto>> GetNdsRatesAsync(int firmId, int userId);

        Task DeleteAsync(int firmId, int userId, long id);

        Task DeleteAsync(int firmId, int userId, IReadOnlyCollection<long> ids);

        Task<SubcontoDto> GetOrCreateTextSubcontoAsync(int firmId, int userId, SubcontoType type, string name);

        Task<List<SubcontoDto>> GetByTypeAutocompleteAsync(int firmId, int userId, SubcontoType type, string query = "",
            int count = 5);
    }
}