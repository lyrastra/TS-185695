using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.SpsV2.Dto.Statistics;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.SpsV2.Client.Statistics
{
    [InjectAsSingleton(typeof(ISpsStatisticsClient))]
    public class SpsStatisticsClient : BaseApiClient, ISpsStatisticsClient
    {
        private readonly SettingValue apiEndpoint;

        private const string CONTROLLER_URI = "/Rest/Statistic";

        public SpsStatisticsClient(IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) 
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ProPrivateApiEndpoint");
        }

        public Task<List<StatDataByLoginDto>> GetStatLastSearchAsync(StatRequestByLoginsData request)
        {
            return PostAsync<StatRequestByLoginsData, List<StatDataByLoginDto>>("/GetStatLastSearch", request);
        }

        public Task<List<StatDataByUserIdDto>> GetStatLastViewAsync(StatRequestByUserIdsData request)
        {
            return PostAsync<StatRequestByUserIdsData, List<StatDataByUserIdDto>>("/GetStatLastView", request);
        }

        public Task<List<StatDataByUserIdsDto>> GetGroupedStatLastViewByIdsListAsync(GroupedStatRequestByUserIds request)
        {
            return PostAsync<GroupedStatRequestByUserIds, List<StatDataByUserIdsDto>>("/GetGroupedStatLastViewByIdsList", request);
        }

        protected override string GetApiEndpoint()
        {
            return $"{apiEndpoint.Value}{CONTROLLER_URI}";
        }
    }
}