using Moedelo.AccountingV2.Dto.PrimaryDocuments;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.PaymentAutomation
{
    [InjectAsSingleton]
    public class PaymentAutomationApiClient : BaseApiClient, IPaymentAutomationApiClient
    {
        private const string ControllerUri = "/PaymentAutomation/";
        private readonly SettingValue apiEndPoint;

        public PaymentAutomationApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(
                httpRequestExecutor,
                uriCreator,
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        public Task<List<PaymentReasonDocumentDto>> GetReasonDocumentsAsync(int firmId, int userId, ReasonDocumentsAutomationDto dto)
        {
            return PostAsync<ReasonDocumentsAutomationDto, List<PaymentReasonDocumentDto>>($"GetReasonDocuments?firmId={firmId}&userId={userId}", dto);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerUri;
        }
    }
}
