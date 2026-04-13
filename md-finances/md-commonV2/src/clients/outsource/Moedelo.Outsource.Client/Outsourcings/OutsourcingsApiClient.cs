using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Outsource.Dto;
using Moedelo.Outsource.Dto.Outsourcings;

namespace Moedelo.Outsource.Client.Outsourcings;

[InjectAsSingleton(typeof(IOutsourcingsApiClient))]
public class OutsourcingsApiClient : BaseCoreApiClient, IOutsourcingsApiClient
{
    private readonly SettingValue apiEndpoint;

    public OutsourcingsApiClient(
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

    public async Task<OutsourcingDto> GetByClientIdAsync(int firmId, int userId, int clientId, int accountId)
    {
        var uri = $"/v1/outsourcings/{clientId}?accountId={accountId}";

        var headers = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
        var response = await GetAsync<ApiDataResponseDto<OutsourcingDto>>(uri, queryHeaders: headers).ConfigureAwait(false);
        return response?.data;
    }

    public async Task<OutsourcingDto[]> GetByClientIdsAsync(IReadOnlyCollection<int> clientIds)
    {
        const string uri = "/v1/outsourcings/GetByIds";

        var headers = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);
        var response = await PostAsync<IReadOnlyCollection<int>, ApiDataResponseDto<OutsourcingDto[]>>(uri, clientIds, queryHeaders: headers).ConfigureAwait(false);
        return response?.data;
    }

    public async Task<int> UpdateAsync(int firmId, int userId, OutsourcingDto dto)
    {
        var uri = $"/v1/outsourcings/{dto.ClientId}";

        var headers = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
        var response = await PutAsync<OutsourcingDto, ApiDataResponseDto<IdValue<int>>>(uri, dto, queryHeaders: headers).ConfigureAwait(false);
        return response?.data?.Id ?? 0;
    }
}