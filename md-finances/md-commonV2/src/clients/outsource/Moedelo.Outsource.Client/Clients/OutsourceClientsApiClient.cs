using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Outsource.Dto;
using Moedelo.Outsource.Dto.Clients;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Outsource.Client.Clients;

[InjectAsSingleton(typeof(IOutsourceClientsApiClient))]
internal sealed class OutsourceClientsApiClient : BaseCoreApiClient, IOutsourceClientsApiClient
{
    private static readonly HttpQuerySetting DontThrowOn404 = new HttpQuerySetting
    {
        DontThrowOn404 = true
    };

    private readonly SettingValue apiEndpoint;

    public OutsourceClientsApiClient(
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
        apiEndpoint = settingRepository.Get("OutsourceClientApiEndpoint");
    }

    protected override string GetApiEndpoint()
    {
        return apiEndpoint.Value;
    }

    public async Task<ClientDto> GetByFirmIdAsync(int firmId, int userId, CancellationToken cancellationToken)
    {
        var uri = $"/v1/firms/{firmId}";

        var headers = await GetPrivateTokenHeaders(firmId, userId, cancellationToken).ConfigureAwait(false);

        var response = await GetAsync<ApiDataResponseDto<ClientDto>>(
                uri, queryHeaders: headers, setting: DontThrowOn404, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        return response?.data;
    }

    public async Task<ClientDto[]> GetByFirmIdsAsync(IReadOnlyCollection<int> firmIds)
    {
        const string uri = "/v1/GetByFirmIds";

        var headers = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);
        var response = await PostAsync<IReadOnlyCollection<int>, ApiDataResponseDto<ClientDto[]>>(uri, firmIds, queryHeaders: headers).ConfigureAwait(false);
        return response?.data;
    }
}