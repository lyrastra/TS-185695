using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.PonyExpress;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CatalogV2.Client.PonyExpress
{
    [InjectAsSingleton]
    public class PonyExpressOfficeApiClient : BaseApiClient, IPonyExpressOfficeApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public PonyExpressOfficeApiClient(
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
            return apiEndPoint.Value + "/PonyExpressOffice/V2";
        }

        public Task<List<PonyExpressOfficeDto>> GetListByRegionIdAsync(int regionId)
        {
            return GetAsync<List<PonyExpressOfficeDto>>("/GetListByRegionId", new {regionId});
        }

        public Task<List<PonyExpressOfficeDto>> GetListAsync()
        {
            return GetAsync<List<PonyExpressOfficeDto>>("/GetList");
        }

        public Task<byte[]> GetFileAsync()
        {
            return GetAsync<byte[]>("/GetFile");
        }
    }
}