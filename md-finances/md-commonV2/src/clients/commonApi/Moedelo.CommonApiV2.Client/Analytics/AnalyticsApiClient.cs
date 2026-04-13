using System;
using System.Threading.Tasks;
using Moedelo.CommonApiV2.Dto.Analytics;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CommonApiV2.Client.Analytics;

[Obsolete("Need to use UserActivityAnalyticsClient")]
[InjectAsSingleton(typeof(IAnalyticsApiClient))]
internal sealed class AnalyticsApiClient : BaseApiClient, IAnalyticsApiClient
{
    private readonly SettingValue apiEndPoint;

    public AnalyticsApiClient(
        IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        IAuditTracer auditTracer,
        IAuditScopeManager auditScopeManager,
        ISettingRepository repository)
        : base(
            httpRequestExecutor,
            uriCreator,
            responseParser,
            auditTracer,
            auditScopeManager)
    {
        apiEndPoint = repository.Get("CommonApiPrivateApiEndpoint");
    }

    protected override string GetApiEndpoint()
    {
        return apiEndPoint.Value;
    }

    public Task SendEventAsync(int firmId, int userId, EventRequest request)
    {
        return PostAsync($"/Analytics/SendEvent?firmId={firmId}&userId={userId}", request);
    }
}
