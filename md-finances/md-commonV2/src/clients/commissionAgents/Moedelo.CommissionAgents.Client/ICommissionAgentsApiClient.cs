using Moedelo.CommissionAgents.Client.Dto;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.CommissionAgents.Client
{
    public interface ICommissionAgentsApiClient : IDI
    {
        Task<IReadOnlyList<CommissionAgentDto>> GetAsync(int firmId, int userId, CommissionAgentRequestDto request, CancellationToken cancellationToken = default);

        Task<bool> CanDeleteAsync(int firmId, int userId, int id);

        /// <summary>
        /// Проверка доступа к разделу "Комиссионеры"
        /// </summary>
        Task<AccessDto> HasAccessAsync(int firmId, int userId);

        /// <summary>
        /// Создание комиссионера по ИНН
        /// </summary>
        Task<CommissionAgentCreateResultDto> CreateByInnAsync(int firmId, int userId, string inn);

        /// <summary>
        /// Есть ли комиссионер с таким ИНН
        /// </summary>
        Task<bool> IsExistsByInnAsync(int firmId, int userId, string inn);

        /// <summary>
        /// Создать комиссионера при сохранении контрагента
        ///
        /// Внимание! Метод временный.
        /// todo: отказаться от сущности, приложения и БД комиссионеров, объединить функционал в контрагентов 
        /// </summary>
        Task<FromKontragent.CreateResultDto> CreateFromKontragentAsync(int firmId, int userId, FromKontragent.CreateRequestDto request);

        /// <summary>
        /// Удалить комиссионера при сохранении контрагента
        ///
        /// Внимание! Метод временный.
        /// todo: отказаться от сущности, приложения и БД комиссионеров, объединить функционал в контрагентов 
        /// </summary>
        Task<FromKontragent.DeleteResultDto> DeleteFromKontragentAsync(int firmId, int userId, FromKontragent.DeleteRequestDto request);
    }
}
