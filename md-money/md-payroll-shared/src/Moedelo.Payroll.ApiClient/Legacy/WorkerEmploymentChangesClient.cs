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
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerEmploymentChanges;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IWorkerEmploymentChangesClient))]
    internal sealed class WorkerEmploymentChangesClient : BaseLegacyApiClient, IWorkerEmploymentChangesClient
    {
        public WorkerEmploymentChangesClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<WorkerEmploymentChangesClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"),
                logger)
        {
        }

        public Task<List<WorkerEmploymentChangeDto>> GetRecruitmentInfosAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return GetAsync<List<WorkerEmploymentChangeDto>>("/WorkerEmploymentChanges/GetRecruitmentInfos",
                new { firmId, userId, startDate, endDate });
        }

        public Task<List<WorkerEmploymentChangeDto>> GetPositionChangesAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return GetAsync<List<WorkerEmploymentChangeDto>>("/WorkerEmploymentChanges/GetPositionChanges",
                new { firmId, userId, startDate, endDate });
        }

        public Task<List<WorkerEmploymentChangeDto>> GetDismissalsAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return GetAsync<List<WorkerEmploymentChangeDto>>("/WorkerEmploymentChanges/GetDismissals",
                new { firmId, userId, startDate, endDate });
        }

        public Task<List<FullWorkerEmploymentChangesDto>> GetFullDataAsync(int firmId, int userId, List<int> workerIds, DateTime beforeDate)
        {
            return PostAsync<FullWorkerEmploymentChangesRequest, List<FullWorkerEmploymentChangesDto>>($"/WorkerEmploymentChanges/GetFullData?firmId={firmId}&userId={userId}",
                new FullWorkerEmploymentChangesRequest(workerIds, beforeDate));
        }

        public Task<List<FullWorkerEmploymentChangesDto>> GetLastChangesOnDateAsync(int firmId, int userId, List<int> workerIds, DateTime beforeDate)
        {
            return PostAsync<FullWorkerEmploymentChangesRequest, List<FullWorkerEmploymentChangesDto>>($"/WorkerEmploymentChanges/GetLastChangesOnDate?firmId={firmId}&userId={userId}",
                new FullWorkerEmploymentChangesRequest(workerIds, beforeDate));
        }

        public Task<List<int>> CheckWorkerChangesInPeriodAsync(int firmId, int userId, 
            IReadOnlyCollection<int> workerIds, DateTime startDate, DateTime endDate)
        {
            return PostAsync<CheckWorkerChangesRequest, List<int>>(
                $"/WorkerEmploymentChanges/CheckWorkerChangesInPeriod?firmId={firmId}&userId={userId}",
                new CheckWorkerChangesRequest(workerIds, startDate, endDate));
        }

        public Task<List<int>> GetWorkerIdsWithoutChangesInPeriodAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return PostAsync<WorkersWithoutChangesRequest, List<int>>(
               $"/WorkerEmploymentChanges/GetWorkerIdsWithoutChangesInPeriod?firmId={firmId}&userId={userId}",
               new WorkersWithoutChangesRequest(startDate, endDate));
        }
    }
}
