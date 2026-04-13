using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeSettings;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SpecialSchedule;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy
{
    public interface IChargeSettingsApiClient
    {
        Task<List<SalaryTemplateDto>> GetSalaryTemplatesAsync(int firmId, int userId, SalaryTemplatesRequestDto request);

        Task<List<AdvanceDto>> GetAdvancesAsync(int firmId, int userId, AdvancesRequestDto request);

        Task<List<SpecialScheduleDto>> GetSpecialSchedulesByCodesAsync(int firmId, int userId, SpecialSchedulesRequestDto request);

        Task<List<VacationDto>> GetVacationsAsync(int firmId, int userId, VacationsRequestDto request);

        Task<bool> IsSpecialScheduleExistAnyAsync(FirmId firmId, UserId userId, SpecialScheduleListRequestDto request);
    }
}