using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccPostings.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccPostings.Client
{
    [InjectAsSingleton]
    public class AnalyticPostingsClient : BaseApiClient, IAnalyticPostingsClient
    {
        private readonly SettingValue apiEndPoint;

        public AnalyticPostingsClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccPostingsApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<List<AnalyticPostingDto>> GetByAsync(int firmId, int userId, AnalyticPostingsSearchCriteriaDto criteria)
        {
            var url = $"/AnalyticPosting/FindByCriteria?firmId={firmId}&userId={userId}";
            return PostAsync<AnalyticPostingsSearchCriteriaDto, List<AnalyticPostingDto>>(url, criteria);
        }
    }
}