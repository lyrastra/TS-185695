using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.Business.Wrappers;
using Moedelo.Money.PaymentOrders.Domain.Models.BudgetaryPayment;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetaryPaymentTaxWidgetRequest = Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment.BudgetaryPaymentTaxWidgetRequest;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(BudgetaryPaymentTaxWidgetApiClient))]
    public sealed class BudgetaryPaymentTaxWidgetApiClient : BaseApiClient
    {
        private const string Prefix = "/private/api/v1";
        private const string Path = "Outgoing/BudgetaryPaymentTaxWidget/GetBudgetaryTaxPayments";

        public BudgetaryPaymentTaxWidgetApiClient(
            ISettingRepository settingRepository,
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ILogger<BudgetaryPaymentTaxWidgetApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("PaymentOrderApiEndpoint"),
                logger)
        {
        }
        
        public async Task<IReadOnlyCollection<OrderTaxWidgetResponse>> GetBudgetaryTaxPaymentsAsync(BudgetaryPaymentTaxWidgetRequest request)
        {
            var result = await PostAsync<BudgetaryPaymentTaxWidgetRequest, ApiDataResponseWrapper<IReadOnlyCollection<OrderTaxWidgetResponse>>>($"{Prefix}/{Path}", request);
            return result.Data;
        }
    }
}
