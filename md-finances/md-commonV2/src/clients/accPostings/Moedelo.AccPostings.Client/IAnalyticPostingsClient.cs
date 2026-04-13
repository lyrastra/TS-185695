using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccPostings.Dto;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccPostings.Client
{
    /// <summary>
    /// Клиент для раздела аналитики
    /// </summary>
    public interface IAnalyticPostingsClient : IDI
    {
        /// <summary>
        /// Получить все проводки по указанному критерию поиска
        /// </summary>
        Task<List<AnalyticPostingDto>> GetByAsync(int firmId, int userId, AnalyticPostingsSearchCriteriaDto criteria);
    }
}