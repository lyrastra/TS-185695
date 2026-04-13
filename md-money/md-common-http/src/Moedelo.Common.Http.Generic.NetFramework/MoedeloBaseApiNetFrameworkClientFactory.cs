using Moedelo.Common.Http.Generic.Abstractions;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Common.Http.Generic.NetFramework;

[InjectAsSingleton(typeof(IMoedeloBaseApiNetFrameworkClientFactory))]
internal sealed class MoedeloBaseApiNetFrameworkClientFactory : IMoedeloBaseApiNetFrameworkClientFactory
{
    private readonly IHttpRequestExecutor httpRequestExecutor;
    private readonly IUriCreator uriCreator; 
    private readonly IResponseParser responseParser;
    private readonly IAuditTracer auditTracer;
    private readonly IAuditScopeManager auditScopeManager;

    public MoedeloBaseApiNetFrameworkClientFactory(
        IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        IAuditTracer auditTracer,
        IAuditScopeManager auditScopeManager)
    {
        this.httpRequestExecutor = httpRequestExecutor;
        this.uriCreator = uriCreator;
        this.responseParser = responseParser;
        this.auditTracer = auditTracer;
        this.auditScopeManager = auditScopeManager;
    }

    public IMoedeloBaseApiClient CreateFor<TApiClient>(SettingValue apiEndpoint)
    {
        return new MoedeloBaseApiNetFrameworkClient(
            httpRequestExecutor,
            uriCreator,
            responseParser,
            auditTracer,
            auditScopeManager,
            apiEndpoint,
            typeof(TApiClient).Name);
    }
}