using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos;

namespace Moedelo.Konragents.ApiClient.Abstractions.legacy
{
    /// <summary>
    /// source: https://github.com/moedelo/md-commonV2/blob/766104b/src/clients/kontragents/Moedelo.KontragentsV2.Client/Kontragents/IKontragentsApiClient.cs
    /// </summary>
    public interface IKontragentsApiClient
    {
        Task<KontragentDto[]> GetAsync(FirmId firmId, UserId userId, HttpQuerySetting setting = null);

        Task<int[]> GetIdsAsync(FirmId firmId, UserId userId, HttpQuerySetting setting = null);
        
        Task<KontragentDto[]> GetByIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<int> ids);

        Task<KontragentDto[]> GetByInnsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<string> inns);
        
        /// <summary>
        /// Возвращает НЕСОХРАНЕННОГО контрагента с предзаполненными по ИНН реквизитами 
        /// </summary>
        Task<KontragentDto> GetByInnsFromOfficeAsync(FirmId firmId, UserId userId, string inn);

        Task<List<BasicKontragentInfoDto>> GetBasicInfoByIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<int> ids);

        /// <summary>
        /// Важно! Данный запрос работает без контекста фирмы! (почему так получилось другой вопрос)
        /// </summary>
        Task<List<BasicKontragentInfoDto>> GetBasicInfoByIdsWithoutContextAsync(IReadOnlyCollection<int> ids);

        Task<int> SaveAsync(FirmId firmId, UserId userId, KontragentDto kontragentDto);
        
        /// <summary>
        /// Сохранение. Аналогично SaveAsync, но возвращает назад модель (можно расширять). 
        /// </summary>
        Task<KontragentSaveResultDto> CreateOrUpdateAsync(FirmId firmId, UserId userId, KontragentDto kontragentDto);

        Task<int> CreatePopulationKontragentIfNotExistAsync(int firmId, int userId);
        Task<List<KontragentDto>> GetByNamesAsync(int firmId, int userId, ICollection<string> names);
        /// <summary>
        /// Возвращает контрагентов по идентификаторам эл. кошельков
        /// </summary>
        Task<List<KontragentDto>> GetByPurseIdsAsync(int firmId, int userId, IReadOnlyCollection<int> purseIds);

        Task<bool> CanDeleteAsync(FirmId firmId, UserId userId, int id);

        Task DeleteAsync(FirmId firmId, UserId userId, int id);

        /// <summary>
        /// Создаёт контрагента Население, если тот не существует и возвращает идентификатор созданного.
        /// Если такой контрагент уже существует, то возвращает идентификатор существующего.
        /// </summary>
        Task<int> GetOrCreatePopulationAsync(FirmId firmId, UserId userId);

        Task<KontragentDto> GetYourselfAsync(FirmId firmId, UserId userId);

        Task<List<KontragentInfoForErptDto>> GetByIdsForErptAsync(IReadOnlyCollection<int> ids);
        Task<List<KontragentInfoForErptDto>> GetAllForErptAsync(FirmId firmId, UserId userId);
        Task<KontragentsPageDto> GetPageAsync(int firmId, int userId, KontragentsPageRequestDto request);
    }
}
