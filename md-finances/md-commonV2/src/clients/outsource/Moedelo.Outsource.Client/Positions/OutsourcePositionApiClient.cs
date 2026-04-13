using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Outsource.Dto;
using Moedelo.Outsource.Dto.Positions;

namespace Moedelo.Outsource.Client.Positions;

[InjectAsSingleton(typeof(IOutsourcePositionApiClient))]
internal sealed class OutsourcePositionApiClient : BaseCoreApiClient, IOutsourcePositionApiClient
{
    private readonly SettingValue apiEndpoint;

    public OutsourcePositionApiClient(
        IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        ISettingRepository settingRepository,
        IAuditTracer auditTracer,
        IAuditScopeManager auditScopeManager)
        : base(
            httpRequestExecutor,
            uriCreator,
            responseParser,
            settingRepository,
            auditTracer,
            auditScopeManager)
    {
        apiEndpoint = settingRepository.Get("OutsourceEmployeeApiEndpoint");
    }

    protected override string GetApiEndpoint()
    {
        return apiEndpoint.Value;
    }
        
    public async Task<int> InsertAsync(int firmId, int userId, PositionPostDto model)
    {
        var uri = $"/v1/positions";
        var headers = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
        var response = await PostAsync<PositionPostDto, ApiDataResponseDto<IdValue<int>>>(uri, model, headers).ConfigureAwait(false);

        return response.data.Id;
    }
}