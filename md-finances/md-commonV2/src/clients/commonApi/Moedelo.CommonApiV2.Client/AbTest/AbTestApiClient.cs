using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CommonApiV2.Dto.AbTest;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CommonApiV2.Client.AbTest;

[InjectAsSingleton(typeof(IAbTestApiClient))]
internal sealed class AbTestApiClient : BaseApiClient, IAbTestApiClient
{
    private readonly SettingValue apiEndPoint;
        
    public AbTestApiClient(IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        ISettingRepository settingRepository,
        IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
        : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
    {
        apiEndPoint = settingRepository.Get("CommonApiPrivateApiEndpoint");
    }

    protected override string GetApiEndpoint()
    {
        return apiEndPoint.Value;
    }

    public Task<AbTestDto> GetAsync(int firmId, int userId, AbTestRequest dto)
    {
        return GetAsync<AbTestDto>($"/AbTest/Get?firmId={firmId}&userId={userId}&PageUrl={dto.PageUrl}");
    }

    public Task<CreateUpdateAbTestDto> CreateOrUpdateAbTestAsync(CreateUpdateAbTestDto requestDto)
    {
        return PostAsync<CreateUpdateAbTestDto, CreateUpdateAbTestDto>("/AbTest/CreateOrUpdate", requestDto);
    }

    public Task<List<CreateUpdateAbTestDto>> GetAllAbTestsAsync()
    {
        return GetAsync<List<CreateUpdateAbTestDto>>("/AbTest/GetAllAbTests");
    }

    public Task<bool> DeleteAbTestAsync(int abTestId)
    {
        return PostAsync<bool>($"/AbTest/DeleteAbTest/?abTestId={abTestId}");
    }

    public Task<CreateUpdateAbTestDto> DeleteAbTestVariantAsync(int abTestVariantId)
    {
        return PostAsync<CreateUpdateAbTestDto>($"/AbTest/DeleteAbTestVariant/?abTestVariantId={abTestVariantId}");
    }

    public Task SetTestVariantAsync(int firmId, int userId, int testId, int variantId)
    {
        return PostAsync($"/AbTest/{testId}/SetUserVariant/{variantId}?firmId={firmId}&userId={userId}");
    }
}
