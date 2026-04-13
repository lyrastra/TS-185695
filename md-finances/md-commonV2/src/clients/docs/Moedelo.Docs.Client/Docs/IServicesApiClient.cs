using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.Dto.Docs;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Docs.Client.Docs
{
    /// <summary>
    /// API услуг, указываемых в документах
    /// </summary>
    public interface IServicesApiClient : IDI
    {
        /// <summary>
        /// Автокомплит по услугам
        /// </summary>
        Task<List<ServiceDto>> GetAutocompleteAsync(int firmId, int userId, string query, int count);

        /// <summary>
        /// Получить услуги по списку имен
        /// </summary>
        Task<List<ServiceDto>> GetByNamesAsync(int firmId, int userId, IReadOnlyCollection<string> names);
    }
}