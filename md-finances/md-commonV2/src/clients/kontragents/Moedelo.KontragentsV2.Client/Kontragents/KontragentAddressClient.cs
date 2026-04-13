using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Kontragents.Address;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.KontragentsV2.Dto.Address;

namespace Moedelo.KontragentsV2.Client.Kontragents
{
    [InjectAsSingleton(typeof(IKontragentAddressClient))]
    public class KontragentAddressClient : BaseApiClient, IKontragentAddressClient
    {
        private readonly SettingValue apiEndpoint;

        public KontragentAddressClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("KontragentsPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<KontragentAddressDto> GetByKontragentIdAndAddressType(int firmId, int userId, int kontragentId, AddressType addressType)
        {
            return GetAsync<KontragentAddressDto>("/Address/GetByKontragentIdAndAddressType", new { firmId, userId, kontragentId, addressType = (int) addressType });
        }
    }
}
