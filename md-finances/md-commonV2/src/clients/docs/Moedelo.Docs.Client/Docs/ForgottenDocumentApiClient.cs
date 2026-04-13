using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.Dto.Docs;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Docs.Client.Docs
{
    [InjectAsSingleton]
    public class ForgottenDocumentApiClient : BaseApiClient, IForgottenDocumentApiClient
    {
        private readonly SettingValue apiEndpoint;

        public ForgottenDocumentApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("DocsApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<List<long>> GetByDate(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return GetAsync<List<long>>("/ForgottenDocument/ByDate", new { firmId, userId, startDate, endDate });
        }

        public Task<List<long>> GetByForgottenDate(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return GetAsync<List<long>>("/ForgottenDocument/ByForgottenDate", new { firmId, userId, startDate, endDate });
        }

        public Task<List<ForgottenDocumentDto>> GetByBaseIds(int firmId, int userId, List<long> baseIds)
        {
            return PostAsync<List<long>, List<ForgottenDocumentDto>>($"/ForgottenDocument/ByBaseIds?firmId={firmId}&userId={userId}", baseIds);
        }
    }
}
