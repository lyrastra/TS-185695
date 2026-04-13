using System;
using System.Management.Instrumentation;
using System.Threading.Tasks;
using Moedelo.ErptV2.Dto.EdsRequest;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.ErptV2.Client.EdsRequests
{
    [InjectAsSingleton]
    public class EdsRequestClient : BaseApiClient, IEdsRequestClient
    {
        private readonly SettingValue apiEndpoint;
        
        public EdsRequestClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,  
            ISettingRepository settingRepository) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ErptApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<EdsRequestChangesDto> GetChangesFromLastSuccessfulRequest(int firmId, int userId)
        {
            return GetAsync<EdsRequestChangesDto>(
                "/EdsRequest/GetChangesFromLastSuccessfulRequest",
                new {firmId, userId});
        }

        public Task<EdsRequestDto> GetEdsRequestFromRequisites(int firmId, int userId)
        {
            return GetAsync<EdsRequestDto>(
                "/EdsRequest/GetEdsRequestFromRequisites",
                new {firmId, userId});
        }

        public Task<EdsEgrRequisitesResponse> GetRequisitesFromEgrAsync(int firmId, int userId, string inn, bool isOoo)
        {
            var settings = new HttpQuerySetting(new TimeSpan(0, 1, 40));
            return GetAsync<EdsEgrRequisitesResponse>("/EdsRequest/GetRequisitesFromEgr", new {firmId, userId, inn, isOoo}, setting: settings);
        }
        
        public Task<EdsRequestDto> GetRequisitesFromLasSuccessfulRequestAsync(int firmId)
        {
            return GetAsync<EdsRequestDto>(
                "/EdsRequest/GetRequisitesFromLasSuccessfulRequest",
                new {firmId});
        }

        public Task<EdsInfoWithChangesDto> GetEdsInfoWithChangesAsync(int historyEventId)
        {
            return GetAsync<EdsInfoWithChangesDto>("/EdsRequest/GetEdsInfoWithChanges", new { historyEventId });
        }
    }
}