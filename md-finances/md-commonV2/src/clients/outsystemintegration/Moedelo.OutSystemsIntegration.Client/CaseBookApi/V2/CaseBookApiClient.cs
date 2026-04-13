using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.OutSystemsIntegrationV2.Dto;
using Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using System;
using Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Pledges;
using Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Executory;

namespace Moedelo.OutSystemsIntegrationV2.Client.CaseBookApi.V2
{
    [InjectAsSingleton]
    public class CaseBookApiClient : BaseApiClient, ICaseBookApiClient
    {
        private readonly SettingValue apiEndpoint;
        private readonly HttpQuerySetting fileSetting = new HttpQuerySetting(new TimeSpan(0, 0, 1, 0));

        public CaseBookApiClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("OutSystemsIntegrationApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<SearchCaseResponseDto> SearchCaseAsync(SearchCaseRequestDto request)
        {
            return GetAsync<SearchCaseResponseDto>("/CaseBookApi/V2/SearchCase", request);
        }

        public async Task<FileResponseDto> GetArchiveAsync(GetArchiveRequestDto request)
        {
            try
            {
                var file = await GetFileAsync("/CaseBookApi/V2/GetArchive", request, setting: fileSetting).ConfigureAwait(false);
                return new FileResponseDto { FileName = file.FileName, DocumentContentType = file.ContentType, DocumentStream = file.Stream };
            }
            catch
            {
                return null;
            }
        }

        public Task<GetOrganizationMessagesResponseDto> GetGetOrganizationMessagesAsync(GetOrganizationMessagesRequestDto request)
        {
            return GetAsync<GetOrganizationMessagesResponseDto>("/CaseBookApi/V2/GetOrganizationMessages", request);
        }

        public Task<GetOrganizationInfoResponseDto> GetOrganizationInfoAsync(GetOrganizationInfoRequestDto request)
        {
            return PostAsync<GetOrganizationInfoRequestDto, GetOrganizationInfoResponseDto>("/CaseBookApi/V2/GetOrganizationInfo", request);
        }
    }
}