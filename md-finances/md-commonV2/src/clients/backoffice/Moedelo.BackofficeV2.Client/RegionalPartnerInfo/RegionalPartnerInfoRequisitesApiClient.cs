using System.Threading.Tasks;
using Moedelo.BackofficeV2.Dto.RegionalPartnerInfo;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BackofficeV2.Client.RegionalPartnerInfo
{
    [InjectAsSingleton]
    public class RegionalPartnerInfoRequisitesApiClient : BaseApiClient, IRegionalPartnerInfoRequisitesApiClient
    {
        private readonly SettingValue apiEndPoint;

        public RegionalPartnerInfoRequisitesApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("BackOfficePrivateApiEndpoint");;
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<RegionalPartnerInfoRequisitesDto> GetAsync(int regionalPartnerInfoId)
        {
            return GetAsync<RegionalPartnerInfoRequisitesDto>($"/Rest/RegionalPartnerInfo/{regionalPartnerInfoId}/Requisites");
        }

        public Task<RegionalPartnerInfoRequisitesDto> GetMoeDeloAsync()
        {
            return GetAsync<RegionalPartnerInfoRequisitesDto>($"/Rest/RegionalPartnerInfo/MoeDelo/Requisites");
        }

        public Task<RegionalPartnerInfoRequisitesDto> GetGlavUchetAsync()
        {
            return GetAsync<RegionalPartnerInfoRequisitesDto>($"/Rest/RegionalPartnerInfo/GlavUchet/Requisites");
        }
        
        public Task<byte[]> GetStampAsync(int regionalPartnerInfoId)
        {
            return GetAsync<byte[]>($"/Rest/RegionalPartnerInfo/{regionalPartnerInfoId}/Stamp");
        }
        
        public Task<byte[]> GetSignAsync(int regionalPartnerInfoId)
        {
            return GetAsync<byte[]>($"/Rest/RegionalPartnerInfo/{regionalPartnerInfoId}/Sign");
        }

        public Task<byte[]> GetSignAsync(int regionalPartnerInfoId, int index)
        {
            return GetAsync<byte[]>($"/Rest/RegionalPartnerInfo/{regionalPartnerInfoId}/Sign/{index}");
        }

        public Task<byte[]> GetLogoAsync(int regionalPartnerInfoId)
        {
            return GetAsync<byte[]>($"/Rest/RegionalPartnerInfo/{regionalPartnerInfoId}/Logo");
        }

        public Task<byte[]> GetMoeDeloStampAsync()
        {
            return GetAsync<byte[]>($"/Rest/RegionalPartnerInfo/MoeDelo/Stamp");
        }

        public Task<byte[]> GetMoeDeloSignAsync(int index)
        {
            return GetAsync<byte[]>($"/Rest/RegionalPartnerInfo/MoeDelo/Sign?index={index}");
        }

        public Task<byte[]> GetMoeDeloLogoAsync()
        {
            return GetAsync<byte[]>($"/Rest/RegionalPartnerInfo/MoeDelo/Logo");
        }

        public Task<byte[]> GetGlavUchetStampAsync()
        {
            return GetAsync<byte[]>($"/Rest/RegionalPartnerInfo/GlavUchet/Stamp");
        }

        public Task<byte[]> GetGlavUchetSignAsync(int index)
        {
            return GetAsync<byte[]>($"/Rest/RegionalPartnerInfo/GlavUchet/Sign?index={index}");
        }
        
        public Task<byte[]> GetGlavUchetLogoAsync()
        {
            return GetAsync<byte[]>($"/Rest/RegionalPartnerInfo/GlavUchet/Logo");
        }
    }
}