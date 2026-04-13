using System;
using System.Threading.Tasks;
using Moedelo.ErptV2.Dto.EdsRequestTasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.ErptV2.Client.EdsRequestTasks
{
#pragma warning disable CS0618 // Type or member is obsolete
    [InjectAsSingleton]
    public class EdsRequestTasksClient : BaseApiClient, IEdsRequestTasksClient
#pragma warning restore CS0618 // Type or member is obsolete
    {
        private readonly SettingValue apiEndpoint;

        public EdsRequestTasksClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ErptApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<EdsRequestTaskResponseDto> GetListAsync(EdsRequestTaskRequestDto request)
        {
            return PostAsync<EdsRequestTaskRequestDto, EdsRequestTaskResponseDto>("/EdsRequestTask/Get", request);
        }

        public Task<EdsRequestTaskDto> GetAsync(int id)
        {
            return GetAsync<EdsRequestTaskDto>($"/EdsRequestTask/Get/{id}");
        }

        public Task SetAsync(EdsRequestTaskSetDto request)
        {
            return PostAsync("/EdsRequestTask/Set", request);
        }

        public Task SetAllOutdatedAsync(int firmId)
        {
            return PostAsync($"/EdsRequestTask/SetAllOutdated?firmId={firmId}");
        }

        public Task ActualizeAsync()
        {
            return PostAsync("/EdsRequestTask/Actualize", setting: new HttpQuerySetting(TimeSpan.FromSeconds(100)));
        }
    }
}