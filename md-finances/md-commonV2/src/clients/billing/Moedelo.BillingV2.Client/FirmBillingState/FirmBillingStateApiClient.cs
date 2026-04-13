using System.Threading;
using System.Threading.Tasks;
using Moedelo.BillingV2.Dto.FirmBillingState;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BillingV2.Client.FirmBillingState
{
    [InjectAsSingleton(typeof(IFirmBillingStateApiClient))]
    public class FirmBillingStateApiClient : BaseApiClient, IFirmBillingStateApiClient
    {
        private readonly SettingValue endpoint;
        
        public FirmBillingStateApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                auditTracer,
                auditScopeManager)
        {
            this.endpoint = settingRepository.Get("InternalBillingApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return endpoint.Value;
        }

        public Task<FirmBillingStateDto> GetActualAsync(int firmId, CancellationToken cancellationToken)
        {
            var uri = $"/FirmState/Actual?firmId={firmId}";
            
            return GetAsync<FirmBillingStateDto>(uri, cancellationToken: cancellationToken);
        }

        public Task<FirmBillingDatesDto> GetBillingDatesAsync(int firmId, CancellationToken cancellationToken)
        {
            var uri = $"/FirmState/BillingDates?firmId={firmId}";
            
            return GetAsync<FirmBillingDatesDto>(uri, cancellationToken: cancellationToken);
        }
    }
}