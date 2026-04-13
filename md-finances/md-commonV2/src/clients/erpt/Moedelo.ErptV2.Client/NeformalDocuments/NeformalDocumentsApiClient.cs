using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Moedelo.ErptV2.Dto.Demands;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.ErptV2.Client.NeformalDocuments
{
    [InjectAsSingleton]
    public class NeformalDocumentsApiClient : BaseApiClient, INeformalDocumentsApiClient
    {
        private readonly SettingValue apiEndpoint;
        
        public NeformalDocumentsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(
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

        public async Task ParseNeformalDocumentWithNotificationAsync(int firmId, int userId, int neformalDocumentId)
        {
            await PostAsync(
                uri:
                $"/ErptNeformal/ParseNeformalDocumentWithNotification?firmId={firmId}&userId={userId}&neformalDocumentId={neformalDocumentId}",
                setting: new HttpQuerySetting(timeout: TimeSpan.FromSeconds(100))
            ).ConfigureAwait(false);
        }
    }
}
