using System;
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
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Audit;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IUnboundPaymentsApiClient))]
    internal sealed class UnboundPaymentsApiClient : BaseLegacyApiClient, IUnboundPaymentsApiClient
    {
        public UnboundPaymentsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,            
            ISettingRepository settingRepository,
            ILogger<UnboundPaymentsApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"), 
                logger)
        {
        }
        
        public Task<List<AuditUnboundPaymentsWorkerDto>> GetUnboundPaymentsWorkersAsync(int firmId, int userId,
            DateTime startDate, DateTime endDate, IReadOnlyCollection<int> workerIds)
        {
            return GetAsync<List<AuditUnboundPaymentsWorkerDto>>("/UnboundPaymentsReport/GetUnboundPaymentsWorkers",
                new {firmId, userId, startDate, endDate, workerIds});
        }

        public Task<List<AuditUnboundPaymentsWorkerDto>> GetUnboundPaymentsWorkersAsync(AuditUnboundPaymentsRequest request)
        {
            return PostAsync<AuditUnboundPaymentsRequest, List<AuditUnboundPaymentsWorkerDto>>(
                $"/UnboundPaymentsReport/GetUnboundPaymentsWorkers?firmId={request.FirmId}&userId={request.UserId}", request);
        }
    }
}
