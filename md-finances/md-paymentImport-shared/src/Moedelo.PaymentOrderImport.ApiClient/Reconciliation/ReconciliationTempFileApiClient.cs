using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Dto;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.Reconciliation;

namespace Moedelo.PaymentOrderImport.ApiClient.Reconciliation;

[InjectAsSingleton(typeof(IReconciliationTempFileApiClient))]
internal sealed class ReconciliationTempFileApiClient : BaseApiClient, IReconciliationTempFileApiClient
{
    private const string UrlPrefix = "/private/api/v1/MovementList/Storage/ReconciliationTempFile";
    
    public ReconciliationTempFileApiClient(
        IHttpRequestExecuter httpRequestExecuter,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<ReconciliationTempFileApiClient> logger)
        : base(
            httpRequestExecuter,
            uriCreator,
            auditTracer,
            authHeadersGetter,
            auditHeadersGetter,
            settingRepository.Get("PaymentImportHandlerApiEndpoint"),
            logger)
    {
    }

    public async Task<string> GetTextAsync(string path, HttpQuerySetting defaultSettings = null)
    {
        var response = await GetAsync<ApiDataDto<string>>(
            $"{UrlPrefix}/GetText",
            new { path },
            setting: defaultSettings);

        return response?.data;
    }

    public async Task<string> SaveAsync(SaveReconciliationTempFileDto dto)
    {
        var response = await PostAsync<SaveReconciliationTempFileDto, ApiDataDto<string>>($"{UrlPrefix}", dto);

        return response?.data;
    }
}