using System;
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
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.MovementListStorage;

namespace Moedelo.PaymentOrderImport.ApiClient.MovementList;

[InjectAsSingleton(typeof(IMovementListIntegrationStorageApiClient))]
internal sealed class MovementListIntegrationStorageApiClient : BaseApiClient, IMovementListIntegrationStorageApiClient
{
    private const string UrlPrefix = "/private/api/v1/MovementList/Storage/Integration";
    
    public MovementListIntegrationStorageApiClient(
        IHttpRequestExecuter httpRequestExecuter,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<MovementListIntegrationStorageApiClient> logger)
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
        return (await GetAsync<ApiDataDto<string>>(
                    $"{UrlPrefix}/GetText",
                    new { path },
                    setting: defaultSettings
                ))?.data;
    }

    public Task<byte[]> GetBytesAsync(string path)
    {
        return GetAsync<byte[]>($"{UrlPrefix}/GetBytes", new { path });
    }

    public async Task<string> SaveAsync(SaveMovementListDto dto)
    {
        return (await PostAsync<SaveMovementListDto, ApiDataDto<string>>($"{UrlPrefix}", dto))?.data;
    }

    public Task RemoveAsync(string path)
    {
        return DeleteAsync($"{UrlPrefix}", new { path });
    }

    public Task<MovementFileInfoDto[]> GetAsync(int firmId)
    {
        return GetAsync<MovementFileInfoDto[]>($"{UrlPrefix}/InfoList", new { firmId });
    }
}