using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SalarySettings;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(ISalarySettingsApiClient))]
    internal sealed class SalarySettingsApiClient : BaseLegacyApiClient, ISalarySettingsApiClient
    {
        public SalarySettingsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SalarySettingsApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"),
                logger)
        {
        }

        public Task<SalarySettingDto> GetSalarySetting(int firmId, int userId)
        {
            return GetAsync<SalarySettingDto>("/SalarySettings/GetSalarySetting", new {firmId, userId});
        }

        public Task<SalarySettingDto> GetSalarySettingOrDefaultByFirmIdAsync(int firmId)
        {
            return GetAsync<SalarySettingDto>("/SalarySettings/GetSalarySettingOrDefaultByFirmId", new { firmId });
        }

        public Task<NdflSettingDto> GetNdflSetting(int firmId, int userId, int ndflSettingId)
        {
            return GetAsync<NdflSettingDto>("/SalarySettings/GetNdflSetting", new {firmId, userId, ndflSettingId});
        }

        public Task<IReadOnlyCollection<NdflSettingDto>> GetNdflSettingList(int firmId, int userId)
        {
            return GetAsync<IReadOnlyCollection<NdflSettingDto>>("/SalarySettings/GetNdflSettingList",
                new {firmId, userId});
        }
    }
}
