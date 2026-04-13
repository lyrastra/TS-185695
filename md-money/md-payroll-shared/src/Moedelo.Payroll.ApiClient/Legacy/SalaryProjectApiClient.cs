using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SalaryProject;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(ISalaryProjectApiClient))]
    internal sealed class SalaryProjectApiClient : BaseLegacyApiClient, ISalaryProjectApiClient
    {
        public SalaryProjectApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SalaryProjectApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"),
                logger)
        {
        }

        public Task<SalaryProjectDto> GetSalaryProject(FirmId firmId, UserId userId)
        {
            return GetAsync<SalaryProjectDto>("/SalaryProjects/GetSalaryProject",
                new { firmId, userId });
        }

        public Task<SalaryProjectDto> GetSalaryProjectByDocumentBaseId(FirmId firmId, UserId userId, long documentBaseId)
        {
            return GetAsync<SalaryProjectDto>("/SalaryProjects/GetSalaryProjectByDocumentBaseId",
                new { firmId, userId, documentBaseId });
        }
    }
}