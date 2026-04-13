using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Outsource.Dto;
using Moedelo.Outsource.Dto.Employees;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.Outsource.Client.Employees;

[InjectAsSingleton(typeof(IOutsourceEmployeeApiClient))]
internal sealed class OutsourceEmployeeApiClient : BaseCoreApiClient, IOutsourceEmployeeApiClient
{
    private static readonly HttpQuerySetting dontThrow404QuerySettings = new HttpQuerySetting
    {
        DontThrowOn404 = true
    };
    private readonly SettingValue apiEndpoint;

    public OutsourceEmployeeApiClient(
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

    public async Task<EmployeeDto> GetAsync(int accountId, int firmId, int userId, CancellationToken cancellationToken)
    {
        var uri = $"/v1/byUserId?mdUserId={userId}&accountId={accountId}";

        var headers = await GetPrivateTokenHeaders(firmId, userId, cancellationToken)
            .ConfigureAwait(false);

        var response = await GetAsync<ApiDataResponseDto<EmployeeDto>>(
                uri, queryHeaders: headers, setting: dontThrow404QuerySettings, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return response?.data;
    }

    public async Task<int> InsertAsync(int accountId, int firmId, int userId, EmployeePostDto dto)
    {
        var uri = $"/v1?accountId={accountId}";
        var headers = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
        var response = await PostAsync<EmployeePostDto, ApiDataResponseDto<IdValue<int>>>(uri, dto, headers);

        return response.data.Id;
    }

    public async Task<EmployeeDto[]> GetByIdsAsync(int accountId, IReadOnlyCollection<int> ids)
    {
        var uri = $"/v1/GetByIds?accountId={accountId}";
        var headers = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);
        var response = await PostAsync<IReadOnlyCollection<int>, ApiDataResponseDto<EmployeeDto[]>>(uri, ids, headers);

        return response.data;
    }

    public async Task<EmployeeDto[]> GetByUserIdsAsync(int accountId, IReadOnlyCollection<int> userIds)
    {
        var uri = $"/v1/GetByUserIds?accountId={accountId}";
        var headers = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);
        var response = await PostAsync<IReadOnlyCollection<int>, ApiDataResponseDto<EmployeeDto[]>>(uri, userIds, headers);

        return response.data;
    }
}