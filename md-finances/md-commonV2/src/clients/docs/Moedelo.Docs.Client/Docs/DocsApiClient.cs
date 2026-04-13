using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.Dto.Docs;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Docs.Client.Docs
{
    [InjectAsSingleton]
    public class DocsApiClient : BaseApiClient, IDocsApiClient
    {
        private readonly SettingValue apiEndpoint;

        public DocsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("DocsApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<List<NotProvideDocumentResultDto>> GetDocumentsNonProvidedInAccountingAsync(
            int firmId,
            int userId,
            DateTime startDate,
            DateTime endDate)
        {
            return GetAsync<List<NotProvideDocumentResultDto>>("/Docs/NonProvidedInAccounting", new { firmId, userId, startDate, endDate });
        }       
    }
}