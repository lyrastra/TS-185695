using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PayrollV2.Client.SalarySettings.DTO;

namespace Moedelo.PayrollV2.Client.PaymentEvents
{
    [InjectAsSingleton]
    public class PaymentEventsApiClient : BaseApiClient, IPaymentEventsApiClient
    {
        private readonly SettingValue apiEndPoint;

        public PaymentEventsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("PayrollPrivateApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/PaymentEvents";
        }
        
        public Task<byte[]> GetPaymentEventFile(int firmId, int? paymentEventFileId)
        {
            return GetAsync<byte[]>("/GetPaymentEventFile", new { firmId, paymentEventFileId });
        }

        public Task<bool> DeletePaymentEventFile(int firmId, int paymentEventFileId)
        {
            return PostAsync<bool>($"/DeletePaymentEventFile?firmId={firmId}&paymentEventFileId={paymentEventFileId}");
        }
    }
}