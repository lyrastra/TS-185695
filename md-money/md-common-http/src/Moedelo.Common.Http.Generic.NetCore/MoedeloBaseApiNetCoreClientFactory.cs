using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Http.Generic.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Common.Http.Generic.NetCore;

[InjectAsSingleton(typeof(IMoedeloBaseApiNetCoreClientFactory))]
internal sealed class MoedeloBaseApiNetCoreClientFactory : IMoedeloBaseApiNetCoreClientFactory
{
    private readonly IHttpRequestExecuter httpRequestExecutor;
    private readonly IUriCreator uriCreator;
    private readonly IAuditTracer auditTracer;
    private readonly IAuthHeadersGetter authHeadersGetter;
    private readonly IAuditHeadersGetter auditHeadersGetter;
    private readonly ILoggerFactory loggerFactory;

    public MoedeloBaseApiNetCoreClientFactory(
        IHttpRequestExecuter httpRequestExecutor,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ILoggerFactory loggerFactory)
    {
        this.httpRequestExecutor = httpRequestExecutor;
        this.uriCreator = uriCreator;
        this.auditTracer = auditTracer;
        this.authHeadersGetter = authHeadersGetter;
        this.auditHeadersGetter = auditHeadersGetter;
        this.loggerFactory = loggerFactory;
    }

    public IMoedeloBaseApiClient CreateFor<TApiClient>(SettingValue apiEndpoint)
    {
        var logger = new Logger<TApiClient>(loggerFactory);
        
        return new MoedeloBaseApiNetCoreClient(
            httpRequestExecutor,
            uriCreator,
            auditTracer,
            authHeadersGetter,
            auditHeadersGetter,
            apiEndpoint,
            logger,
            typeof(TApiClient).Name);
    }
}