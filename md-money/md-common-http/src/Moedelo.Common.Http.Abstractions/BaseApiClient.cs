using System;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Common.Http.Abstractions
{
    public abstract class BaseApiClient : BaseApiClientInternal
    {
        private static readonly HttpQuerySetting DefaultSettingValue = new HttpQuerySetting(TimeSpan.FromSeconds(10));
        
        protected BaseApiClient(
            IHttpRequestExecuter httpRequestExecutor,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            SettingValue endpointSetting,
            ILogger logger,
            string auditTypeName = null)
            : base(
                httpRequestExecutor,
                uriCreator,
                auditTracer,
                new IDefaultHeadersGetter[] {authHeadersGetter, auditHeadersGetter},
                endpointSetting,
                logger,
                auditTypeName)
        {
        }

        protected sealed override HttpQuerySetting DefaultSetting => DefaultSettingValue;
    }
}