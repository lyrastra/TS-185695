using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SalaryTemplates;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(ISalaryTemplateApiClient))]
    public class SalaryTemplateApiClient : BaseLegacyApiClient, ISalaryTemplateApiClient
    {
        public SalaryTemplateApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SalaryTemplateApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"),
                logger)
        {
        }

        public Task<IReadOnlyCollection<SalaryTemplateDto>> GetByWorkerIdAsync(int firmId, int userId, int workerId,
            CancellationToken token = default)
        {
            return GetAsync<IReadOnlyCollection<SalaryTemplateDto>>("/SalaryTemplate/ByWorkerId",
                new { firmId, userId, workerId }, cancellationToken: token);
        }
    }
}