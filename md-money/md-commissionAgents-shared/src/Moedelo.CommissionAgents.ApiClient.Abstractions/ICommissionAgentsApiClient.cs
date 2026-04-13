using Moedelo.CommissionAgents.ApiClient.Abstractions.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.CommissionAgents.ApiClient.Abstractions
{
    public interface ICommissionAgentsApiClient
    {
        /// <summary>
        /// Получение комиссионера по ИД контрагента
        /// </summary>
        Task<CommissionAgentDto> GetByKontragentIdAsync(int kontragentId);

        /// <summary>
        /// Получение комиссионера по ИНН
        /// </summary>
        Task<CommissionAgentDto> GetByInnAsync(string inn);

        /// <summary>
        /// Получение списка комиссионеров для автокомплита
        /// </summary>
        Task<IReadOnlyCollection<CommissionAgentDto>> GetAutocompleteAsync(string query, int count);

        /// <summary>
        /// Доступ к комиссионерам
        /// </summary>
        Task<AccessDto> HasAccessAsync();

        /// <summary>
        /// Получение комиссионера по Id
        /// </summary>
        Task<CommissionAgentDto> GetByIdAsync(int id);

        /// <summary>
        /// Получение комиссионеров по списку идентификаторов
        /// </summary>
        Task<IReadOnlyList<CommissionAgentDto>> GetByIdsAsync(IReadOnlyList<int> ids);
    }
}
