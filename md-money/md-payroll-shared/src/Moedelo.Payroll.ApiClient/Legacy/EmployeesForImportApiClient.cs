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
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IEmployeesForImportApiClient))]
    internal sealed class EmployeesForImportApiClient : BaseLegacyApiClient, IEmployeesForImportApiClient
    {
        public EmployeesForImportApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<EmployeesForImportApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollApi"),
                logger)
        {

        }

        public async Task<WorkerForPaymentImportDto[]> GetAsync(FirmId firmId, UserId userId)
        {
            var results =
                await PostAsync<DataResponseWrapper<WorkerForPaymentImportDto[]>>(
                        $"/EmployeesRestApi/GetForPaymentImport?firmId={firmId}&userId={userId}")
                    .ConfigureAwait(false);
            return results.Data;
        }
    }
}