using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.NdsDeclarationSection7Code;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CatalogV2.Client.NdsDeclarationSection7Code
{
    [InjectAsSingleton]
    public class NdsDeclarationSection7CodeApiClient : BaseApiClient, INdsDeclarationSection7CodeApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public NdsDeclarationSection7CodeApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager
                 )
        {
            apiEndPoint = settingRepository.Get("CatalogApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/NdsDeclarationSection7Code/V2";
        }

        public Task<List<NdsDeclarationSection7CodeDto>> GetByIds(IReadOnlyCollection<int> ids)
        {
            if (ids?.Any() != true)
            {
                return Task.FromResult(new List<NdsDeclarationSection7CodeDto>());
            }

            return PostAsync<IReadOnlyCollection<int>, List<NdsDeclarationSection7CodeDto>>("/GetByIds", ids);
        }

        public Task<List<NdsDeclarationSection7CodeDto>> GetByCodes(IReadOnlyCollection<string> codes)
        {
            if (codes?.Any() != true)
            {
                return Task.FromResult(new List<NdsDeclarationSection7CodeDto>());
            }

            return PostAsync<IReadOnlyCollection<string>, List<NdsDeclarationSection7CodeDto>>("/GetByCodes", codes);
        }
    }
}
