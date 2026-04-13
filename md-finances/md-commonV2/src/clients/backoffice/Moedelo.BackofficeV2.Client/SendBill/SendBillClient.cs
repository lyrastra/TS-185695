using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BackofficeV2.Dto.SendBill;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BackofficeV2.Client.SendBill
{
    [InjectAsSingleton(typeof(ISendBillClient))]
    public class SendBillClient : BaseApiClient, ISendBillClient
    {
        private readonly SettingValue apiEndPoint;
        
        public SendBillClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("BackOfficePrivateApiEndpoint");
        }

        public Task<List<SendBillResponseDto>> SendAsync(List<SendBillRequestDto> sendBillDtos)
        {
            return PostAsync<List<SendBillRequestDto>, List<SendBillResponseDto>>("/Rest/SendBill/V2/Send", sendBillDtos);
        }

        public Task<string> ResendBillAsync(ResendBillRequestDto dto)
        {
            return PostAsync<ResendBillRequestDto, string>("/Rest/SendBill/V2/Resend", dto);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}