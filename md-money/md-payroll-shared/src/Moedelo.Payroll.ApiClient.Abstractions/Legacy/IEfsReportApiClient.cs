using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Common;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.EfsExperience;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.Employment;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.Injured;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy
{
    public interface IEfsReportApiClient
    {
        Task<EfsEmploymentDto> GetEfsEmploymentDataAsync(FirmId firmId, UserId userId, DateTime reportDate);

        [Obsolete]
        Task<List<EfsPositionChangesDto>> GetEfsPositionChangesAsync(FirmId firmId, UserId userId, int year, int month);

        /// <summary>
        /// Информация о должностях, которые были у заданных сотрудников в заданном месяце
        /// </summary>
        Task<IReadOnlyCollection<EfsPositionChangesDto>> GetEfsPositionDataAsync(FirmId firmId, UserId userId,
            EfsPositionDataRequestDto dto);

        /// <summary>
        /// Список индентификаторов сотрудников, у которых в указанном месяце были изменения должностей
        /// </summary>
        Task<IReadOnlyCollection<int>> GetEfsPositionChangesWorkerIdsAsync(FirmId firmId, UserId userId, int year,
            int month);

        Task<List<EfsEmploymentWorkerDto>> GetEfsWorkersDataAsync(FirmId firmId, UserId userId, EfsWorkersDataRequest dto);

        Task<EfsInjuredDto> GetEfsInjuredInitialDataAsync(FirmId firmId, UserId userId, int year, int periodNumber);

        Task<EfsExperienceInitialDataDto> GetEfsExperienceInitialDataAsync(FirmId firmId, UserId userId, int year, int? workerId = null);

        Task<EfsExperienceInitialDataDto> GetEfsChildCareInitialDataAsync(FirmId firmId, UserId userId, DateTime reportDate);

        Task<bool> HasEfsChildCareWorkersAsync(FirmId firmId, UserId userId, DateTime reportDate);
        
        Task<IReadOnlyCollection<DateTime>> GetEfsInjuredWorkPeriodsAsync(FirmId firmId, UserId userId, PeriodDto request);
    }
}