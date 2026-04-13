using System;
using System.Threading.Tasks;
using Moedelo.BizV2.Dto.PaymentOrder;
using Moedelo.BizV2.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BizV2.Client.PaymentOrder
{
    [InjectAsSingleton]
    public class PaymentOrderClient : BaseApiClient, IPaymentOrderClient
    {
        private readonly SettingValue apiEndPoint;
        
        public PaymentOrderClient(
            IHttpRequestExecutor httpRequestExecutor,
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
            apiEndPoint = settingRepository.Get("BizPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<SendPaymentOrderResponseDto> SendPaymentOrderAsync(SendPaymentOrderRequestDto dto)
        {
            return PostAsync<SendPaymentOrderRequestDto, SendPaymentOrderResponseDto>($"/PaymentOrder/SendPaymentOrder?firmId={dto.FirmId}&userId={dto.UserId}", dto);
        }

        public async Task<byte[]> GetSberbankReceiptAsync(int firmId, int userId, GetSberbankReceiptDto dto)
        {
            var result = await PostAsync<GetSberbankReceiptDto, string>($"/PaymentOrder/GetSberbankReceipt?firmId={firmId}&userId={userId}", dto).ConfigureAwait(false);
            return Convert.FromBase64String(result);
        }

        public async Task<byte[]> DownloadPaymentAsync(int firmId, int userId, DownloadPaymentDto dto)
        {
            var result = await PostAsync<DownloadPaymentDto, DataWrapper<string>>($"/PaymentOrder/DownloadPayment?firmId={firmId}&userId={userId}", dto).ConfigureAwait(false);
            return Convert.FromBase64String(result.Data);
        }

        public async Task<byte[]> DownloadReceiptAsync(int firmId, int userId, DownloadPaymentDto dto)
        {
            var result = await PostAsync<DownloadPaymentDto, DataWrapper<string>>($"/PaymentOrder/DownloadReceipt?firmId={firmId}&userId={userId}", dto).ConfigureAwait(false);
            return Convert.FromBase64String(result.Data);
        }
    }
}