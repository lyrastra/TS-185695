using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.Common.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Models;

namespace Moedelo.Money.ApiClient.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentTaxWidgetApiClient))]
    public class BudgetaryPaymentTaxWidgetApiClient : BaseApiClient, IBudgetaryPaymentTaxWidgetApiClient
    {
        public BudgetaryPaymentTaxWidgetApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<BudgetaryPaymentApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("MoneyApiEndpoint"),
                logger)
        {
        }

        public async Task<List<OrderTaxWidgetResponse>> GetBudgetaryTaxPaymentsAsync(BudgetaryPaymentNdsWidgetRequest request)
        {
            var url = $"/api/v1/PaymentOrders/Outgoing/BudgetaryPaymentsTaxWidget/GetBudgetaryTaxPayments/";
            var result = await PostAsync<BudgetaryPaymentNdsWidgetRequest, ApiDataDto<List<OrderTaxWidgetResponse>>>(url, request);
            return result.data;
        }
    }
}