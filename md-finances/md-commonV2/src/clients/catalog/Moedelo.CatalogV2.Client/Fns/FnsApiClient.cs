using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.Fns;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CatalogV2.Client.Fns
{
    [InjectAsSingleton(typeof(IFnsApiClient))]
    public class FnsApiClient : BaseApiClient, IFnsApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public FnsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager
                 )
        {
            apiEndPoint = settingRepository.GetRequired("CatalogApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Fns/V2";
        }

        public Task<FnsDto> GetByIdAsync(int id, CancellationToken ct)
        {
            return GetAsync<FnsDto>("/GetById", new {id}, cancellationToken: ct);
        }

        public Task<FnsDto> GetByCodeAsync(string code)
        {
            return GetAsync<FnsDto>("/GetByCode", new { code });
        }

        public Task<List<FnsDto>> GetByCodesAsync(IReadOnlyCollection<string> codes)
        {
            return PostAsync<IReadOnlyCollection<string>, List<FnsDto>>("/GetByCodes", codes);
        }

        public Task<List<FnsDto>> GetByRegionAsync(string regionCode, CancellationToken ct)
        {
            return GetAsync<List<FnsDto>>("/GetByRegion", new {regionCode}, cancellationToken: ct);
        }

        public Task<FnsWithRequisitesDto> GetWithRequisitesAsync(int id, string oktmo)
        {
            return GetAsync<FnsWithRequisitesDto>("/GetWithRequisites", new { id, oktmo });
        }

        public Task<FnsWithRequisitesDto> GetWithRequisitesByCodeAndOktmoAsync(string code, string oktmo)
        {
            return GetAsync<FnsWithRequisitesDto>("/GetWithRequisitesByCodeAndOktmo", new { code, oktmo });
        }

        public Task<FnsRequisitesDto> GetRequisitesByIdAndOktmoAsync(int id, string oktmo)
        {
            return GetAsync<FnsRequisitesDto>("/GetRequisitesByIdAndOktmo", new { id, oktmo });
        }

        public Task<FnsRequisitesDto> GetRequisitesByCodeAndOktmoAsync(string code, string oktmo)
        {
            return GetAsync<FnsRequisitesDto>("/GetRequisitesByCodeAndOktmo", new { code, oktmo });
        }
    }
}