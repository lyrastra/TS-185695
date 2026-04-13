using System.Threading.Tasks;
using Moedelo.BillingV2.Dto;
using Moedelo.BillingV2.Dto.PaymentShift;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BillingV2.Client.PaymentShiftApi
{
    [InjectAsSingleton(typeof(IPaymentShiftApiClient))]
    public class PaymentShiftApiClient : BaseApiClient, IPaymentShiftApiClient
    {
        private readonly SettingValue apiEndPoint;

        public PaymentShiftApiClient(
           IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
           : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("InternalBillingApiEndpoint");
        }
        public Task ShiftPaymentAsync(PaymentShiftRequestDto dto)
        {
            return PostAsync("/PaymentShift/Shift", dto);
        }

        public Task<BaseDto> ShiftSuccessBackofficeBillingTrial(BackofficeBillingShiftRequestDto dto)
        {
            return PostAsync<BackofficeBillingShiftRequestDto, BaseDto>("/PaymentShift/SuccessBackofficeBillingTrial", dto);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}
