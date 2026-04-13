using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Receipts;
using Moedelo.Billing.Abstractions.Receipts.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Billing.ApiClient.NetFramework.Receipts;

[InjectAsSingleton(typeof(IReceiptsApiClient))]
public class ReceiptsApiClient : BaseApiClient, IReceiptsApiClient
{
    private readonly SettingValue apiEndPoint;

    public ReceiptsApiClient(
        IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        ISettingRepository settingRepository,
        IAuditTracer auditTracer,
        IAuditScopeManager auditScopeManager)
        : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
    {
        apiEndPoint = settingRepository.Get("BillingReceiptsApiEndpoint");
    }

    public Task SendAsync(ReceiptSendRequestDto requestDto)
    {
        const string uri = "/v1/receipt/send";

        return PostAsync(uri, requestDto);
    }

    protected override string GetApiEndpoint()
    {
        return apiEndPoint.Value;
    }
}