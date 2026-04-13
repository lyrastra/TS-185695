using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SpsV2.Dto.Advertisements;

namespace Moedelo.SpsV2.Client.Advertisements
{
    [InjectAsSingleton(typeof(IAdvertisementsApiClient))]
    public class AdvertisementsApiClient : BaseApiClient, IAdvertisementsApiClient
    {
        private readonly SettingValue apiEndPoint;

        private const string controllerUri = "/Rest/Advertisements";

        public AdvertisementsApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) 
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("ProPrivateApiEndpoint");
        }

        public Task<AdvertisementDto> GetActiveAdvertisementAsync()
        {
            return GetAsync<AdvertisementDto>("/GetActiveAdvertisement");
        }

        protected override string GetApiEndpoint()
        {
            return $"{apiEndPoint.Value}{controllerUri}";
        }
    }
}