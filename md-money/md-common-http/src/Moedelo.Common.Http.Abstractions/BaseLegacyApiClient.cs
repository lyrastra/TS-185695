using System;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Common.Http.Abstractions;

public abstract class BaseLegacyApiClient : BaseApiClientInternal
{
    private static TimeSpan DefaultTimeout => TimeSpan.FromSeconds(30);
    private static readonly HttpQuerySetting DefaultSettingValue = new HttpQuerySetting(DefaultTimeout);

    protected BaseLegacyApiClient(
        IHttpRequestExecuter httpRequestExecutor,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuditHeadersGetter auditHeadersGetter,
        SettingValue endpointSetting,
        ILogger logger,
        string auditTypeName = null)
        : base(
            httpRequestExecutor,
            uriCreator,
            auditTracer,
            [auditHeadersGetter],
            endpointSetting,
            logger,
            auditTypeName)
    {
    }

    protected sealed override HttpQuerySetting DefaultSetting => DefaultSettingValue;
}
