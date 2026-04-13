using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RptV2.Dto;
using Moedelo.RptV2.Dto.TradingTax;

namespace Moedelo.RptV2.Client.TradingTax
{
    [InjectAsSingleton]
    public class TradingTaxPaymentClient : BaseApiClient, ITradingTaxPaymentClient
    {
        private readonly SettingValue apiEndpoint;

        public TradingTaxPaymentClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : base(
                httpRequestExecutor, 
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("RptApiEndpoint");
        }

        public async Task<TradingTaxPaymentDto> GetPaymentDataAsync(int firmId, int userId, int eventId)
        {
            return (await PostAsync<DataWrapper<TradingTaxPaymentDto>>($"/TradingTaxPayment/GetPaymentData?firmId={firmId}&userId={userId}&eventId={eventId}").ConfigureAwait(false)).Data;
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }
    }
}
