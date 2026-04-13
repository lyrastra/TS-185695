using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeSettings;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SpecialSchedule;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IChargeSettingsApiClient))]
    internal sealed class ChargeSettingsApiClient : BaseLegacyApiClient, IChargeSettingsApiClient
    {
        public ChargeSettingsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<ChargeSettingsApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"),
                logger)
        {
        }

        public Task<List<SalaryTemplateDto>> GetSalaryTemplatesAsync(int firmId, int userId,
            SalaryTemplatesRequestDto request)
        {
            return PostAsync<SalaryTemplatesRequestDto, List<SalaryTemplateDto>>(
                $"/ChargeSettings/GetSalaryTemplates?firmId={firmId}&userId={userId}",
                request);
        }

        public Task<List<AdvanceDto>> GetAdvancesAsync(int firmId, int userId, AdvancesRequestDto request)
        {
            return PostAsync<AdvancesRequestDto, List<AdvanceDto>>(
                $"/ChargeSettings/GetAdvances?firmId={firmId}&userId={userId}", request);
        }

        public Task<List<SpecialScheduleDto>> GetSpecialSchedulesByCodesAsync(int firmId, int userId,
            SpecialSchedulesRequestDto request)
        {
            return PostAsync<SpecialSchedulesRequestDto, List<SpecialScheduleDto>>(
                $"/ChargeSettings/GetSpecialSchedulesByCodes?firmId={firmId}&userId={userId}", request);
        }

        public Task<List<VacationDto>> GetVacationsAsync(int firmId, int userId, VacationsRequestDto request)
        {
            return PostAsync<VacationsRequestDto, List<VacationDto>>(
                $"/ChargeSettings/GetVacations?firmId={firmId}&userId={userId}", request);
        }

        public Task<bool> IsSpecialScheduleExistAnyAsync(FirmId firmId, UserId userId,
            SpecialScheduleListRequestDto request)
        {
            return PostAsync<SpecialScheduleListRequestDto, bool>(
                $"/ChargeSettings/IsSpecialScheduleExistAny?firmId={firmId}&userId={userId}", request);
        }
    }
}