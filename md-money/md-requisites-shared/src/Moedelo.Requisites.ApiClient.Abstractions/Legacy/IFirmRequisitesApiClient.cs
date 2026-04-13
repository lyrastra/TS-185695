using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy
{
    public interface IFirmRequisitesApiClient
    {
        Task<FirmRequisitesDto> GetAsync(FirmId firmId);

        Task<IReadOnlyCollection<FirmRequisitesDto>> GetFirmsByIdsAsync(IReadOnlyCollection<int> firmIds);

        Task<RegistrationDataDto> GetRegistrationDataAsync(FirmId firmId, UserId userId);

        Task<IReadOnlyCollection<FirmRegistrationDataDto>> GetRegistrationDataAsync(
            IReadOnlyCollection<int> firmIds,
            CancellationToken cancellationToken);

        Task<RegistrationShortDataDto[]> GetRegistrationShortDataAsync(IReadOnlyCollection<int> firmIds);

        /// <summary>
        /// Читает все фирмы которые на осно на текущий момент исключая тестовых
        /// </summary>
        /// <returns></returns>
        Task<RegistrationShortDataDto[]> GetOsnoRegistrationShortDataAsync(int year);

        Task<DirectorDto> GetDirectorAsync(FirmId firmId, UserId userId);

        Task<IReadOnlyCollection<int>> GetFirmIdsByInn(string inn);

        Task SaveRegistrationDataAsync(FirmId firmId, UserId userId, RegistrationDataDto data);

        Task SetEmployerModeAsync(FirmId firmId, UserId userId, bool isEmployerMode);

        Task SaveDirectorAsync(FirmId firmId, UserId userId, DirectorDto director);

        Task SetDirectorAsync(FirmId firmId, UserId userId, int directorId);

        Task SetInFaceAsync(FirmId firmId, UserId userId, DirectorSetInFaceDto dto);

        Task FillByInnAsync(FirmId firmId, UserId userId, string inn, bool withDirector = true, CancellationToken? cancellationToken = null);

        Task<FirmShortInfoDto[]> GetFirmShortInfosAsync(IReadOnlyCollection<int> firmIds);

        /// <summary>
        /// Получить краткую информацию о фирмах на основе даты регистрации в сервисе.
        /// </summary>
        /// <param name="date">Дата регистрации фирм в сервисе</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IReadOnlyList<FirmShortInfoDto>> GetFirmShortInfosRegisteredOnAsync(DateTime date,
            CancellationToken cancellationToken);

        /// <summary>
        /// Получить список моделей с базовой информацией о фирме (не блокируется при дебаге на проде)
        /// </summary>
        /// <param name="firmIds">Список идентификаторов фирм</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Список моделей с базовой информацией о фирме</returns>
        Task<FirmShortInfoWithoutPhoneDto[]> GetFirmShortInfosWithoutPhoneAsync(
            IReadOnlyCollection<int> firmIds, CancellationToken cancellationToken = default);

        Task SavePassportDataAsync(FirmId firmId, UserId userId, PassportDataDto data);

        Task<IReadOnlyDictionary<int, int?>> GetRegionIdsByFirmIdsAsync(IReadOnlyCollection<int> firmIds);
        Task<PassportDataDto> GetPassportDataAsync(FirmId firmId, UserId userId);

        /// <summary>
        /// Получение данных для поздравления руководителя фирмы с днем рождения
        /// </summary>
        /// <param name="firmIds">Идентификаторы искомых фирм (обязательный параметр)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Список на основе которого будут получены данные о днях рождениях руководителей фирм</returns>
        Task<List<DirectorBirthdayDataDto>> GetDirectorBirthdayDataAsync(IReadOnlyCollection<int> firmIds, CancellationToken cancellationToken = default);

        Task<RequisitesForFormDto> GetRequisitesForFormByFirmId(int firmId);
    }
}