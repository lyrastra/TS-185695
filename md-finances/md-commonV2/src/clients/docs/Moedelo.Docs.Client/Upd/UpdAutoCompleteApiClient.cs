using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.Dto.Upd;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Docs.Client.Upd
{
    [InjectAsSingleton]
    public class UpdAutoCompleteApiClient : BaseApiClient, IUpdAutoCompleteApiClient
    {
        private readonly SettingValue apiEndpoint;

        public UpdAutoCompleteApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) :
            base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager
            )
        {
            apiEndpoint = settingRepository.Get("DocsApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<List<UpdAsPossibleInvoiceReasonResponseDto>> GetPossibleInvoiceReasonAsync(int firmId, int userId, UpdAsPossibleInvoiceReasonRequestDto requestDto)
        {
            var requestParams = new
            {
                firmId,
                userId,
                requestDto.Query,
                requestDto.KontragentId,
                Direction = (int)requestDto.Direction,
                requestDto.Count,
                SinceDate = requestDto.SinceDate?.ToString("yyyy-MM-dd")
            };

            return GetAsync<List<UpdAsPossibleInvoiceReasonResponseDto>>("/Upd/AutoComplete/PossibleInvoiceReasons", requestParams);
        }

        public Task<List<UpdAsReasonForPaymentResponseDto>> ReasonDocumentForPayment(int firmId, int userId, UpdAsReasonForPaymentRequestDto requestDto)
        {
            var requestParams = new
            {
                firmId,
                userId,
                requestDto.BillBaseId,
                requestDto.ContractBaseId,
                requestDto.Count,
                requestDto.KontragentId,
                requestDto.Direction,
                requestDto.Query,
                requestDto.PaymentBaseId,
                requestDto.KontragentAccountCode
            };
            return GetAsync<List<UpdAsReasonForPaymentResponseDto>>("/Upd/AutoComplete/ReasonDocumentForPayment", requestParams);
        }
    }
}