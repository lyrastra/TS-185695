using System;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Dto.BizToAccTransfer;
using Moedelo.Billing.Abstractions.Dto.BizToAccTransfer.SwitchOn;
using Moedelo.Billing.Abstractions.Interfaces.BizToAccTransfer;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Billing.ApiClient.NetFramework.BizToAccTransfer
{
    [InjectAsSingleton(typeof(IBizToAccBillingTransferApiClient))]
    public class BizToAccBillingTransferApiClient : BaseApiClient, IBizToAccBillingTransferApiClient
    {
        private readonly SettingValue apiEndPoint;

        public BizToAccBillingTransferApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("InternalBillingApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<TransferValidation.ResultDto> ValidateAsync(int fromFirmId)
        {
            const string uri = "/BizToAccTransfer/Validate";
            var dto = new TransferValidation.RequestDto { FirmId = fromFirmId };

            return PostAsync<TransferValidation.RequestDto, TransferValidation.ResultDto>(uri, dto);
        }

        public Task<ContextDto> GetContextAsync(int fromFirmId)
        {
            const string uri = "/BizToAccTransfer/Context";

            return GetAsync<ContextDto>(uri, new { firmId = fromFirmId });
        }

        public Task<TransferPayment.ResultDto> TransferPaymentAsync(TransferPayment.RequestDto dto)
        {
            const string uri = "/BizToAccTransfer/TransferPayment";
            const int queryTimeout = 90;
            var settings = BuildTimeoutQuierySettings(queryTimeout);

            return PostAsync<TransferPayment.RequestDto, TransferPayment.ResultDto>(uri, dto, setting: settings);
        }

        public Task RollbackPaymentTransactionsAsync(RollbackPaymentTransactionsDto dto)
        {
            const string uri = "/BizToAccTransfer/RollbackPaymentTransactions";
            const int queryTimeout = 90;

            if (dto?.PaymentMaps?.Any() != true)
            {
                return Task.CompletedTask;
            }

            var settings = BuildTimeoutQuierySettings(queryTimeout);

            return PostAsync(uri, dto, setting: settings);
        }

        public Task<ExpiredAccessPayment.ResultDto> SetExpiredAccessPaymentAsync(int paymentId)
        {
            const string uri = "/BizToAccTransfer/ExpiredAccessPayment/Set";
            const int queryTimeout = 60;
            var settings = BuildTimeoutQuierySettings(queryTimeout);
            var dto = new ExpiredAccessPayment.RequestDto { PaymentId = paymentId };

            return PostAsync<ExpiredAccessPayment.RequestDto, ExpiredAccessPayment.ResultDto>(uri, dto, setting: settings);
        }

        public Task RollbackSetExpiredAccessPaymentAsync(ExpiredAccessPayment.ResultDto dto)
        {
            const string uri = "/BizToAccTransfer/ExpiredAccessPayment/Rollback";
            const int queryTimeout = 60;

            if (dto == null)
            {
                return Task.CompletedTask;
            }

            var settings = BuildTimeoutQuierySettings(queryTimeout);

            return PostAsync (uri, dto, setting: settings);
        }

        public Task<TransferedPaymentMapDto> SwithOnTransferredPaymentAsync(SwitchOnTransferredPaymentRequestDto dto)
        {
            const string uri = "/BizToAccTransfer/SwitchOnTransferredPayment";
            const int queryTimeout = 90;
            var settings = BuildTimeoutQuierySettings(queryTimeout);

            return PostAsync<SwitchOnTransferredPaymentRequestDto, TransferedPaymentMapDto>(uri, dto, setting: settings);
        }

        public Task<Payment.ResultDto> SetPaymentAsync(Payment.RequestDto dto)
        {
            const string uri = "/BizToAccTransfer/Payment/Set";
            const int queryTimeout = 60;
            var settings = BuildTimeoutQuierySettings(queryTimeout);

            return PostAsync<Payment.RequestDto, Payment.ResultDto>(uri, dto, setting: settings);
        }

        public Task RollbackSetPaymentAsync(Payment.ResultDto dto)
        {
            const string uri = "/BizToAccTransfer/Payment/Rollback";
            const int queryTimeout = 60;

            if (dto == null)
            {
                return Task.CompletedTask;
            }

            var settings = BuildTimeoutQuierySettings(queryTimeout);

            return PostAsync (uri, dto, setting: settings);
        }

        private static HttpQuerySetting BuildTimeoutQuierySettings(int timeoutInseconds)
        {
            var timeout = TimeSpan.FromSeconds(timeoutInseconds);

            return new HttpQuerySetting
            {
                Timeout = timeout
            };
        }
    }
}