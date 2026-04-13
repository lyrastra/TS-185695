using Moedelo.HomeV2.Dto.PaymentShift;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HomeV2.Client.PaymentShift
{
    [InjectAsSingleton]
    public class PaymentShiftApiClient : BaseApiClient, IPaymentShiftApiClient
    {
        private readonly SettingValue apiEndPoint;

        public PaymentShiftApiClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager
            )
        {
            apiEndPoint = settingRepository.Get("InternalBillingApiEndpoint");
        }

        public Task ShiftPaymentAsync(PaymentShiftRequestDto dto)
        {
            return PostAsync<PaymentShiftRequestDto>("/Shift", dto);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/PaymentShift";
        }
    }
}
