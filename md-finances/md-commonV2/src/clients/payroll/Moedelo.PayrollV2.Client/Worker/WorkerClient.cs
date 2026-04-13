using System;
using System.Collections.Generic;
using System.Threading;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.PayrollV2.Dto.Worker;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.PayrollV2.Client.Worker
{
    [InjectAsSingleton]
    public class WorkerClient : BaseApiClient, IWorkerClient
    {
        private static readonly HttpQuerySetting CreateUserQuerySetting = new HttpQuerySetting(TimeSpan.FromSeconds(120));

        private readonly SettingValue apiEndPoint;

        public WorkerClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("PayrollPrivateApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Worker";
        }

        public Task<int> CreateAsync(CreateWorkerDto model, int firmId, int userId)
        {
            return PostAsync<CreateWorkerDto, int>($"/Create?firmId={firmId}&userId={userId}", model, setting: CreateUserQuerySetting);
        }

        public Task<int> GetWorkersMaxTableNumberAsync(int firmId, int userId, CancellationToken cancellationToken)
        {
            var uri = $"/GetMaxTableNumber?firmId={firmId}&userId={userId}";

            return GetAsync<int>(uri, cancellationToken: cancellationToken);
        }

        public Task UpdateAsync(UpdateWorkerDto model, int firmId, int userId)
        {
            return PostAsync<UpdateWorkerDto>($"/Update?firmId={firmId}&userId={userId}", model);
        }

        public Task<List<FirmWorkersCountDto>> GetWorkersCountForLimitsAsync(IReadOnlyCollection<int> firmIds)
        {
            return PostAsync<IReadOnlyCollection<int>, List<FirmWorkersCountDto>>("/GetWorkersCountForLimits", firmIds);
        }

        public Task<List<WorkPeriodsDto>> GetWorkPeriodsAsync(IReadOnlyCollection<int> workerIds, int firmId, int userId)
        {
            return PostAsync<IReadOnlyCollection<int>, List<WorkPeriodsDto>>
                ($"/GetWorkPeriods?firmId={firmId}&userId={userId}", workerIds);
        }

        public Task<WorkerDto> GetWorkerAsync(int firmId, int userId, int workerId, CancellationToken cancellationToken)
        {
            var uri = $"/GetWorker?firmId={firmId}&userId={userId}&workerId={workerId}";

            return GetAsync<WorkerDto>(uri, cancellationToken: cancellationToken);
        }
    }
}