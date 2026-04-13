using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Address.Dto.FirmAddress;

namespace Moedelo.Address.Client.FirmAddress
{
    [InjectAsSingleton(typeof(IFirmAddressApiClient))]
    public class FirmAddressApiClient : BaseApiClient, IFirmAddressApiClient
    {
        private readonly SettingValue apiEndPoint;

        public FirmAddressApiClient(
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
            apiEndPoint = settingRepository.Get("AddressApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task SaveAsync(int firmId, AddressRequestModel dto)
        {
            return PostAsync($"/FirmAddress?firmId={firmId}", dto);
        }

        public Task<AddressResponseModel> GetAsync(int firmId)
        {
            return GetAsync<AddressResponseModel>($"/FirmAddress", new { firmId });
        }
    }
}
