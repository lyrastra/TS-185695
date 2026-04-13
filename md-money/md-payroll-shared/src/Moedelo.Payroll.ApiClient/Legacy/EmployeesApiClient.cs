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
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IEmployeesApiClient))]
    internal class EmployeesApiClient : BaseLegacyApiClient, IEmployeesApiClient
    {
        public EmployeesApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<EmployeesApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollApi"),
                logger)
        {
        }

        public async Task<WorkerDto> GetByIdAsync(FirmId firmId, UserId userId, int workerId)
        {
            var uri = $"/EmployeesRestApi/GetWorker?firmId={firmId}&userId={userId}";
            var response = await PostAsync<object, DataResponseWrapper<WorkerDto>>(uri, new { workerId });
            return response.Data;
        }

        public async Task<List<WorkerDto>> GetByIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<int> workerIds)
        {
            if (workerIds.Count == 0)
            {
                return new List<WorkerDto>();
            }

            var uri = $"/EmployeesRestApi/GetWorkers?firmId={firmId}&userId={userId}";
            var response = await PostAsync<IReadOnlyCollection<int>, WorkerListDto>(uri, workerIds);
            return response.Workers;
        }

        public async Task<WorkerDto> GetDirectorAsync(FirmId firmId, UserId userId)
        {
            var uri = $"/EmployeesRestApi/GetDirector?firmId={firmId}&userId={userId}";
            var response = await GetAsync<DataResponseWrapper<WorkerDto>>(uri);
            return response.Data;
        }

        public Task<WorkerCardAccountDto> GetWorkerCardAccountAsync(FirmId firmId, UserId userId, int workerId)
        {
            var uri = $"/EmployeesRestApi/GetWorkerCardAccount?firmId={firmId}&userId={userId}";
            return PostAsync<object, WorkerCardAccountDto>(uri, new { workerId });
        }

        public async Task<List<WorkerCardAccountDto>> GetWorkersCardAccountAsync(FirmId firmId, UserId userId,
            IEnumerable<int> workerIds)
        {
            var uri = $"/EmployeesRestApi/GetWorkersCardAccount?firmId={firmId}&userId={userId}";
            var data = await PostAsync<object, DataResponseWrapper<List<WorkerCardAccountDto>>>(uri, new { workerIds });
            return data.Data;
        }
    }
}
