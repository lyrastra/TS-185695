using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.Reconciliation;

namespace Moedelo.PaymentOrderImport.ApiClient.Reconciliation;

[InjectAsSingleton(typeof(IReconciliationResultApiClient))]
internal sealed class ReconciliationResultApiClient : BaseApiClient, IReconciliationResultApiClient
{
    private const string UrlPrefix = "/private/api/v1/ReconciliationResult";
    
    public ReconciliationResultApiClient(IHttpRequestExecuter httpRequestExecuter,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<ReconciliationResultApiClient> logger) 
        : base(httpRequestExecuter,
            uriCreator,
            auditTracer,
            authHeadersGetter,
            auditHeadersGetter,
            settingRepository.Get("PaymentImportHandlerApiEndpoint"),
            logger)
    {
    }

    public Task InsertAsync(ReconciliationResultDto dto)
    {
        return PostAsync($"{UrlPrefix}", dto);
    }

    public Task UpdateAsync(ReconciliationResultDto dto)
    {
        return PutAsync($"{UrlPrefix}", dto);
    }

    public Task<ReconciliationResultDto> GetAsync(Guid sessionId, int firmId)
    {
        return GetAsync<ReconciliationResultDto>($"{UrlPrefix}", new {sessionId, firmId});
    }
}