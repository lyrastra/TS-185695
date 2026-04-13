using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.CommonApi.ApiClient.Abstractions.legacy.FirmFlags;
using Moedelo.CommonApi.ApiClient.Abstractions.legacy.FirmFlags.Dtos;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.CommonApi.ApiClient.Legacy.FirmFlags;

[InjectAsSingleton(typeof(IFirmFlagsApiClient))]
internal sealed class FirmFlagsApiClient : BaseLegacyApiClient, IFirmFlagsApiClient
{
    public FirmFlagsApiClient(
        IHttpRequestExecuter httpRequestExecuter,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<FirmFlagsApiClient> logger)
        : base(
            httpRequestExecuter,
            uriCreator,
            auditTracer,
            auditHeadersGetter,
            settingRepository.Get("CommonApiPrivateApiEndpoint"),
            logger)
    {
    }

    public Task<FirmFlagDto[]> GetAsync(FirmId firmId, CancellationToken cancellationToken)
    {
        return GetAsync<FirmFlagDto[]>($"/FirmFlags/?firmId={firmId}", cancellationToken: cancellationToken);
    }

    public Task<bool> IsEnableAsync(FirmId firmId, UserId userId, string name,
        CancellationToken cancellationToken)
    {
        const string uri = "/FirmFlags/IsEnable";
        return GetAsync<bool>(uri, new { firmId, userId, name }, cancellationToken: cancellationToken);
    }

    public Task EnableAsync(FirmId firmId, UserId userId, string name)
    {
        return PostAsync($"/FirmFlags/Enable?firmId={firmId}&userId={userId}&name={name}");
    }

    public Task DisableAsync(FirmId firmId, UserId userId, string name)
    {
        return PostAsync($"/FirmFlags/Disable?firmId={firmId}&userId={userId}&name={name}");
    }

    public Task RemoveAsync(FirmId firmId, UserId userId, string name)
    {
        return DeleteAsync($"/FirmFlags/Remove?firmId={firmId}&userId={userId}&name={name}");
    }
}
