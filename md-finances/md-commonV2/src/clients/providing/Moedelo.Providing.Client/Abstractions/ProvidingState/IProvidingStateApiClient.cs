using System.Threading.Tasks;
using Moedelo.Providing.Client.Abstractions.ProvidingState.Models;

namespace Moedelo.Providing.Client.Abstractions.ProvidingState
{
    /// <note>
    /// Внимание! Актуальная версия находится в md-providing-shared (не может использоваться в net framework)
    /// </note>
    public interface IProvidingStateApiClient
    {
        /// <summary>
        /// Количество активных записей (когда проведение в процессе)
        /// </summary>
        Task<int> GetActiveCountAsync(int firmId, int userId);

        /// <summary>
        /// Добавляет запись о начале "проведения" документа
        /// </summary>
        /// <returns>Идентификатор записи</returns>
        Task<int> SetAsync(int firmId, int userId, SetStateRequestDto request);

        /// <summary>
        /// Очищает запись о "проведении" документа
        /// </summary>
        Task UnsetAsync(int firmId, int userId, long stateId);
    }
}
