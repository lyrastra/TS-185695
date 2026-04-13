using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.Municipal;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CatalogV2.Client.Municipal
{
    [InjectAsSingleton(typeof(IMunicipalApiClient))]
    public class MunicipalApiClient : BaseApiClient, IMunicipalApiClient
    {
        private readonly SettingValue apiEndPoint;

        public MunicipalApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.GetRequired("CatalogApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Municipal/V2";
        }

        public Task<MunicipalDto> GetByIdAsync(int id)
        {
            return GetAsync<MunicipalDto>("/GetById", new {id});
        }

        public Task<MunicipalDto> GetByOktmoAsync(string oktmo, CancellationToken ctx)
        {
            return GetAsync<MunicipalDto>("/GetByOktmo", new {oktmo}, cancellationToken: ctx);
        }

        public Task<List<MunicipalDto>> GetByIfnsAsync(string ifns)
        {
            return GetAsync<List<MunicipalDto>>("/GetByIfns", new { ifns });
        }

        public Task<List<MunicipalDto>> GetByIfnsListAsync(IReadOnlyCollection<string> ifnsList)
        {
            return PostAsync<IReadOnlyCollection<string>, List<MunicipalDto>>("/GetByIfnsList", ifnsList);
        }
    }
}