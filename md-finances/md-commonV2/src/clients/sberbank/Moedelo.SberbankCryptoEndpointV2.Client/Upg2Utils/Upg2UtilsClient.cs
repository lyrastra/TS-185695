using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SberbankCryptoEndpointV2.Dto.Upg2Utils;

namespace Moedelo.SberbankCryptoEndpointV2.Client.Upg2Utils
{
    [InjectAsSingleton]
    public class Upg2UtilsClient : BaseApiClient, IUpg2UtilsClient
    {
        private readonly SettingValue apiEndpoint;

        public Upg2UtilsClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("SberbankCryptoEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/Upg2UtilsV2/";
        }

        public Task<GetActiveSberbankCertificateResponseDto> GetActiveSberbankCertificateAsync()
        {
            return GetAsync<GetActiveSberbankCertificateResponseDto>("GetActiveSberbankCertificate");
        }

        public Task<bool> GetIsWorkingAsync()
        {
            return GetAsync<bool>("GetIsWorking");
        }
        
        public Task<string> TestGreenTokenAsync()
        {
            return GetAsync<string>("TestGreenToken");
        }
    }
}