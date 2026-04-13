using System.Collections.Generic;
using System.Linq;
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
    public class ServicesApiClient : BaseApiClient, IServicesApiClient
    {
        private readonly SettingValue apiEndpoint;

        public ServicesApiClient(
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

        public Task<List<ServiceDto>> GetAutocompleteAsync(int firmId, int userId, string query, int count)
        {
            return GetAsync<List<ServiceDto>>("/Services/Autocomplete", new { firmId, userId, query, count });
        }

        public Task<List<ServiceDto>> GetByNamesAsync(int firmId, int userId, IReadOnlyCollection<string> names)
        {
            if (names?.Any() != true)
            {
                return Task.FromResult(new List<ServiceDto>());
            }
            
            return PostAsync<IReadOnlyCollection<string>, List<ServiceDto>>(
                $"/Services/ByNames?firmId={firmId}&userId={userId}", 
                names);
        }
    }
}