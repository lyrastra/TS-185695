using System;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Dto.BizToAccTransfer.RevertTransfer;
using Moedelo.Billing.Abstractions.Dto.BizToAccTransfer.RevertTransfer.Context;
using Moedelo.Billing.Abstractions.Dto.BizToAccTransfer.RevertTransfer.State;
using Moedelo.Billing.Abstractions.Interfaces.BizToAccTransfer.RevertTransfer;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Billing.ApiClient.NetFramework.BizToAccTransfer.RevertTransfer
{
    [InjectAsSingleton(typeof(IRevertBizToAccTransferBillingApiClient))]
    public class RevertBizToAccTransferBillingApiClient : BaseApiClient, IRevertBizToAccTransferBillingApiClient
    {
        private readonly SettingValue apiEndPoint;

        public RevertBizToAccTransferBillingApiClient(
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

        public Task<RevertTransferContextDto> GetRevertContextAsync(RevertTransferContextRequestDto dto)
        {
            var uri = "/RevertBizToAccTransfer/Context";
            var settings = new HttpQuerySetting { Timeout = TimeSpan.FromMinutes(2) };

            return GetAsync<RevertTransferContextDto>(uri, dto, setting: settings);
        }

        public Task<SwitchOffAccPaymentStateDto> SwitchOffAccPaymentAsync(int accPaymentId)
        {
            var uri = $"/RevertBizToAccTransfer/SwitchOffAccPayment?{nameof(accPaymentId)}={accPaymentId}";
            var settings = new HttpQuerySetting { Timeout = TimeSpan.FromMinutes(2) };

            return PostAsync<SwitchOffAccPaymentStateDto>(uri, setting: settings);
        }

        public Task RollbackSwitchOffAccPaymentAsync(SwitchOffAccPaymentStateDto prevState)
        {
            var uri = $"/RevertBizToAccTransfer/SwitchOffAccPayment/Rollback";
            var settings = new HttpQuerySetting { Timeout = TimeSpan.FromMinutes(2) };

            return PostAsync(uri, prevState, setting: settings);
        }

        public Task<RevertTransferPaymentContextDto> GetRevertTransferPaymentContextAsync(int paymentId)
        {
            var uri = "/RevertBizToAccTransfer/Context/Payment";
            var settings = new HttpQuerySetting { Timeout = TimeSpan.FromMinutes(2) };

            return GetAsync<RevertTransferPaymentContextDto>(uri, new { paymentId }, setting: settings);
        }

        public Task<SwitchOnBizPaymentStateDto> SwitchOnBizPaymentAsync(SwitchOnBizPaymentRequestDto requestDto)
        {
            var uri = $"/RevertBizToAccTransfer/SwitchOnBizPayment";
            var settings = new HttpQuerySetting { Timeout = TimeSpan.FromMinutes(2) };

            return PostAsync<SwitchOnBizPaymentRequestDto, SwitchOnBizPaymentStateDto>(uri, requestDto, setting: settings);
        }

        public Task RollbackSwitchOnBizPaymentAsync(SwitchOnBizPaymentStateDto prevState)
        {
            var uri = $"/RevertBizToAccTransfer/SwitchOnBizPayment/Rollback";
            var settings = new HttpQuerySetting { Timeout = TimeSpan.FromMinutes(2) };

            return PostAsync(uri, prevState, setting: settings);
        }
    }
}
