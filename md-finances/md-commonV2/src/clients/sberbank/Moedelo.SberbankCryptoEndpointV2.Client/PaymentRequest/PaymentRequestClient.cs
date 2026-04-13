using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.Response;
using Moedelo.SberbankCryptoEndpointV2.Dto.PaymentRequest;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using System;

namespace Moedelo.SberbankCryptoEndpointV2.Client.PaymentRequest
{
    [InjectAsSingleton]
    public class PaymentRequestClient : BaseApiClient, IPaymentRequestClient
    {
        private readonly SettingValue apiEndpoint;

        public PaymentRequestClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("SberbankCryptoEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/PaymentRequest/";
        }

        public Task<SendPaymentRequestResponseDto> SendPaymentRequestAsync(SendPaymentRequestDto dto, HttpQuerySetting httpQuerySetting = null)
        {
            return PostAsync<SendPaymentRequestDto, SendPaymentRequestResponseDto>("SendPaymentRequest", dto, null, httpQuerySetting);
        }

        public Task<GetSberbankPaymentRequestsStatusResponseDto> GetPaymentRequestStatusAsync(GetPaymentRequestStatusRequestDto dto)
        {
            return PostAsync<GetPaymentRequestStatusRequestDto, GetSberbankPaymentRequestsStatusResponseDto>("GetPaymentRequestStatus", dto);
        }

        public Task<GetAllAdvanceAcceptancesResponseDto> GetAllAdvanceAcceptancesAsync(GetAllAdvanceAcceptancesRequestDto dto)
        {
            return PostAsync<GetAllAdvanceAcceptancesRequestDto, GetAllAdvanceAcceptancesResponseDto>("GetAllAdvanceAcceptances", dto, setting: new HttpQuerySetting(TimeSpan.FromMinutes(2)));
        }
        
        public Task<string> GetRequestAndResponseAllAdvanceAcceptancesAsync(GetAllAdvanceAcceptancesRequestDto dto)
        {
            return PostAsync<GetAllAdvanceAcceptancesRequestDto, string>("GetRequestAndResponseAllAdvanceAcceptances", dto);
        }
    }
}