using System.Threading.Tasks;
using System.Collections.Generic;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.ErptV2.Dto.FnsNeformalDocuments;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.ErptV2.Client.NeformalDocuments
{
    [InjectAsSingleton]
    public class FnsNeformalDocumentsClient : BaseApiClient, IFnsNeformalDocumentsClient
    {
        private readonly SettingValue apiEndpoint;
        
        public FnsNeformalDocumentsClient(
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
            apiEndpoint = settingRepository.Get("ErptApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<List<FnsNeformalDocumentsCountForFirmDto>> GetCountsForFirmsAsync(List<int> firmIds)
        {
            return PostAsync<List<int>, List<FnsNeformalDocumentsCountForFirmDto>>(
                "/FnsNeformalDocuments/GetCountsForFirms", firmIds);
        }
    }
}
