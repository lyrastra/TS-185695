using System.Threading.Tasks;
using Moedelo.ErptV2.Dto.AutoComplete;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.ErptV2.Client.AutoComplete
{
    [InjectAsSingleton]
    public class ErptAutocompleteClient : BaseApiClient, IErptAutocompleteClient
    {
        private readonly SettingValue apiEndpoint;
        
        public ErptAutocompleteClient(
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

        public async Task<GetNdsXmlFilesResponse> GetNdsXmlFilesAutocomplete(int firmId, int userId, GetNdsXmlFilesRequest request)
        {
            return await PostAsync<GetNdsXmlFilesRequest, GetNdsXmlFilesResponse>(
                "/ErptAutocomplete/GetNdsXmlFilesAutocomplete?firmId={firmId}&userId={userId}", request).ConfigureAwait(false);
        }
    }
}
