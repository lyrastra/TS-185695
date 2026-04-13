using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Providing.ApiClient.Abstractions.ProvidingState.Models;
using System.Threading.Tasks;

namespace Moedelo.Providing.ApiClient.Abstractions.ProvidingState
{
    public interface IProvidingStateApiClient
    {
        /// <summary>
        /// Количество активных записей (когда проведение в процессе)
        /// </summary>
        Task<int> GetActiveCountAsync();

        /// <summary>
        /// Добавляет запись о начале "проведения" документа
        /// </summary>
        /// <returns>Идентификатор записи</returns>
        Task<long> SetAsync(SetStateRequestDto request, HttpQuerySetting setting = null);

        /// <summary>
        /// Очищает КОНКРЕТНУЮ запись о "проведении" документа
        /// </summary>
        Task UnsetAsync(long stateId);

        /// <summary>
        /// Очищает записи о "проведении" документа (вызывать при удалении док-та)
        /// </summary>
        Task UnsetByBaseIdAsync(long baseId);
    }
}
