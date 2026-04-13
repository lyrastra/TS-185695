using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money.PaymentOrders;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using System.Threading.Tasks;

namespace Moedelo.Finances.Business.Services.Money.Operations.PaymentOrders
{
    /// <inheritdoc cref="IPaymentOrderOperationUpdater"/>
    [InjectAsSingleton]
    public class PaymentOrderOperationUpdater : BaseCoreApiClient, IPaymentOrderOperationUpdater
    {
        private readonly ISettingRepository settingRepository;

        public PaymentOrderOperationUpdater(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            this.settingRepository = settingRepository;
        }

        protected override string GetApiEndpoint()
        {
            return settingRepository.Get("MoneyApiEndpoint").Value;
        }

        public async Task ApproveImportedAsync(int firmId, int userId, MoneySourceType sourceType = MoneySourceType.All, long? sourceId = null)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            await PostAsync(
                "/api/v1/Operations/Imported/Approve",
                new
                {
                    SourceId = sourceId,
                    SourceType = sourceType != MoneySourceType.All
                        ? (MoneySourceType?)sourceType
                        : null
                },
                queryHeaders: tokenHeaders).ConfigureAwait(false);
        }
    }
}
