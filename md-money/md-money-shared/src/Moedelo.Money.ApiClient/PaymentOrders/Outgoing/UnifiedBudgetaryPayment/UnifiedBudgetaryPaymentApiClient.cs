using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Exceptions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.Common.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Dto;

namespace Moedelo.Money.ApiClient.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;

[InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentApiClient))]
public class UnifiedBudgetaryPaymentApiClient : BaseApiClient, IUnifiedBudgetaryPaymentApiClient
{
    public UnifiedBudgetaryPaymentApiClient(
        IHttpRequestExecuter httpRequestExecuter,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<UnifiedBudgetaryPaymentApiClient> logger)
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

    public async Task<string> GetDescriptionAsync(IReadOnlyCollection<UnifiedBudgetarySubPaymentDto> subPaymentsDto)
    {
        const string url = $"/api/v1/PaymentOrders/Outgoing/UnifiedBudgetaryPayment/GetDescription";
        var result = await PostAsync<IReadOnlyCollection<UnifiedBudgetarySubPaymentDto>, ApiDataDto<string>>(url, subPaymentsDto);
        return result.data;
    }

    public async Task<PaymentOrderSaveResponseDto> CreateAsync(UnifiedBudgetaryPaymentSaveDto dto)
    {
        const string url = $"/api/v1/PaymentOrders/Outgoing/UnifiedBudgetaryPayment/";
        var result = await PostAsync<UnifiedBudgetaryPaymentSaveDto, ApiDataDto<PaymentOrderSaveResponseDto>>(url, dto);
        return result.data;
    }

    public async Task<UnifiedBudgetaryPaymentDto> GetAsync(long documentBaseId)
    {
        var url = $"/api/v1/PaymentOrders/Outgoing/UnifiedBudgetaryPayment/{documentBaseId}";
        try
        {
            var result = await GetAsync<ApiDataDto<UnifiedBudgetaryPaymentDto>>(url);
            return result.data;
        }
        catch (HttpRequestResponseStatusException e)
        {
            if (e.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            throw;
        }
    }

    public Task DeleteAsync(long documentBaseId)
    {
        var url = $"/api/v1/PaymentOrders/Outgoing/UnifiedBudgetaryPayment/{documentBaseId}";

        try
        {
            return DeleteAsync(url);
        }
        catch (HttpRequestResponseStatusException e)
        {
            if (e.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            throw;
        }
    }
}