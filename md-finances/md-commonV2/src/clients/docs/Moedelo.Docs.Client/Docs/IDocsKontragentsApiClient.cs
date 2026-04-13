using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.Docs.Client.Docs
{
    /// <summary>
    /// Методы, связанные с контрагентами
    /// </summary>
    public interface IDocsKontragentsApiClient : IDI
    {
        /// <summary>
        /// Проверить, используются ли контрагенты в первичных документах
        /// </summary>
        /// <returns>
        /// Список Id контрагентов, которые используются в документах
        /// </returns>
        Task<List<int>> GetUsedInDocsAsync(int firmId, int userId, IReadOnlyCollection<int> kontragentIds);
    }
}