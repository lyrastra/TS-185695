using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.BPM.Registry.Documents.Client.Models;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BPM.Registry.Documents.Client
{
    [InjectAsSingleton]
    public class DocumentsRegistryApiClient : BaseApiClient, IDocumentsRegistryApiClient
    {
        private readonly SettingValue apiEndpoint;

        public DocumentsRegistryApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("BPMApiUrl");
        }
        
        public async Task<RegistryDto[]> GetAsync(int firmId, CategoryTypeDto? category = null, int? operationType = null, bool? completed = null)
        {
            var result = await GetAsync<WrapperDto<RegistryDto[]>>($"/{firmId}", new { category, operationType, completed }).ConfigureAwait(false);

            return result?.data;
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/registries/documents";
        }
    }
}
