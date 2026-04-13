using System.Threading;
using System.Threading.Tasks;
using Moedelo.CommonApiV2.Dto.Support;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CommonApiV2.Client.Support;

[InjectAsSingleton(typeof(ISupportDataClient))]
internal sealed class SupportDataClient : BaseApiClient, ISupportDataClient
{
    private readonly SettingValue apiEndPoint;

    public SupportDataClient(
        IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        IAuditTracer auditTracer,
        IAuditScopeManager auditScopeManager,
        ISettingRepository settingRepository)
        : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
    {
        apiEndPoint = settingRepository.Get("CommonApiPrivateApiEndpoint");
    }

    protected override string GetApiEndpoint()
    {
        return apiEndPoint.Value;
    }

    public Task<SupportDataDto> GetAsync(int firmId, int userId, CancellationToken cancellationToken)
    {
        var uri = $"/SupportData/Get?firmId={firmId}&userId={userId}";

        return GetAsync<SupportDataDto>(uri, cancellationToken: cancellationToken);
    }
}