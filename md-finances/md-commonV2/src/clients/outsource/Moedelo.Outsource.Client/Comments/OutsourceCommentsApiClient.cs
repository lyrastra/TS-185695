using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Outsource.Dto;
using Moedelo.Outsource.Dto.Comments;

namespace Moedelo.Outsource.Client.Comments;

[InjectAsSingleton(typeof(IOutsourceCommentsApiClient))]
public class OutsourceCommentsApiClient : BaseCoreApiClient, IOutsourceCommentsApiClient
{
    private readonly SettingValue apiEndpoint;

    public OutsourceCommentsApiClient(
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
        
    public async Task<IReadOnlyList<CommentDto>> GetListAsync(int accountId, int groupId, int tagId)
    {
        var uri = $"/v1?GroupId={groupId}&AccountId={accountId}&TagsIds={tagId}";
        var headers = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);
        var response = await GetAsync<ApiDataResponseDto<IReadOnlyList<CommentDto>>>(uri, queryHeaders: headers);

        return response?.data;
    }
}