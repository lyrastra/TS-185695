using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Business.Wrappers;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing.BudgetaryPayment;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments
{
    [InjectAsSingleton(typeof(BudgetaryPaymentCurrencyInvoiceNdsApiClient))]
    internal sealed class BudgetaryPaymentCurrencyInvoiceNdsApiClient : BaseApiClient
    {
        private const string prefix = "/private/api/v1/Outgoing/BudgetaryPayment";
        private static readonly HttpQuerySetting DefaultHttpSetting = new HttpQuerySetting(TimeSpan.FromSeconds(15));

        public BudgetaryPaymentCurrencyInvoiceNdsApiClient(
            ISettingRepository settingRepository,
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ILogger<PaymentOrderApiClient> logger)
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

        public async Task<IReadOnlyCollection<CurrencyInvoiceNdsPaymentResponse>> GetCurrencyInvoiceNdsPaymentsByAsync(CurrencyInvoiceNdsPaymentsRequestDto request)
        {
            var result = await PostAsync<CurrencyInvoiceNdsPaymentsRequestDto, ApiDataResponseWrapper<IReadOnlyCollection<CurrencyInvoiceNdsPaymentResponseDto>>>(
                $"{prefix}/GetCurrencyInvoiceNdsPayments",
                request,
                setting: DefaultHttpSetting);

            return result.Data
                .Select(MapToResult)
                .ToArray();
        }

        private static CurrencyInvoiceNdsPaymentResponse MapToResult(CurrencyInvoiceNdsPaymentResponseDto payment)
        {
            return new CurrencyInvoiceNdsPaymentResponse
            {
                DocumentBaseId = payment.DocumentBaseId,
                Date = payment.Date,
                Number = payment.Number,
                Sum = payment.Sum
            };
        }
    }
}
