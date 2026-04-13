using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Address.ApiClient.Abstractions.legacy;
using Moedelo.Address.ApiClient.Abstractions.legacy.Dto.FirmAddress;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Address.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IFirmAddressApiClient))]
    public class FirmAddressApiClient : BaseApiClient, IFirmAddressApiClient
    {
        public FirmAddressApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<FirmAddressApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("AddressApiEndpoint"),
                logger)
        {
        }

        public Task<AddressResponseModel> GetAsync(int firmId)
        {
            return GetAsync<AddressResponseModel>($"/FirmAddress", new { firmId });
        }
    }
}