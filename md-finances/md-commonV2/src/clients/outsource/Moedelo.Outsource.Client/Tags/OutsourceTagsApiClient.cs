using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Outsource.Dto;
using Moedelo.Outsource.Dto.Tags;

namespace Moedelo.Outsource.Client.Tags;

[InjectAsSingleton(typeof(IOutsourceTagsApiClient))]
internal sealed class OutsourceTagsApiClient : BaseCoreApiClient, IOutsourceTagsApiClient
{
    private readonly SettingValue apiEndpoint;

    public OutsourceTagsApiClient(
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
        apiEndpoint = settingRepository.Get("OutsourceCommentApiEndpoint");
    }

    protected override string GetApiEndpoint()
    {
        return apiEndpoint.Value;
    }
        
    public async Task<IReadOnlyList<TagDto>> GetListAsync(int accountId)
    {
        var uri = $"/v1/Tags/{accountId}";
        var headers = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);
        var response = await GetAsync<ApiDataResponseDto<IReadOnlyList<TagDto>>>(uri, queryHeaders: headers);
            
        return response.data;
    }
}