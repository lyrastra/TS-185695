using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.OutSystemsIntegrationV2.Dto;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi;
using Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Pledges;
using Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Executory;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using System;

namespace Moedelo.OutSystemsIntegrationV2.Client.CaseBookApi.V3
{
    [InjectAsSingleton]
    public class CaseBookApiClient : BaseApiClient, ICaseBookApiClient
    {
        private readonly SettingValue _apiEndpoint;
        private readonly HttpQuerySetting _pledgeSettings = new HttpQuerySetting(TimeSpan.FromSeconds(90));
        private readonly HttpQuerySetting _fileSettings = new HttpQuerySetting(TimeSpan.FromSeconds(60));

        public CaseBookApiClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            _apiEndpoint = settingRepository.Get("OutSystemsIntegrationApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return _apiEndpoint.Value;
        }

        public async Task<SearchCaseResponseDto> SearchCaseAsync(SearchCaseRequestDto request)
        {
            return await GetAsync<SearchCaseResponseDto>("/CaseBookApi/V3/SearchCase", request).ConfigureAwait(false);
        }

        public async Task<SearchCaseResponseDto> GetCitizenBankruptcyAsync(GetCitizenBankruptcyRequestDto request)
        {
            return await GetAsync<SearchCaseResponseDto>("/CaseBookApi/V3/GetCitizenBankruptcy", request).ConfigureAwait(false);
        }
        
        public async Task<FileResponseDto> GetArchiveAsync(GetArchiveRequestDto request)
        {
            try
            {
                var file = await GetFileAsync("/CaseBookApi/V3/GetArchive", request, setting: _fileSettings).ConfigureAwait(false);
                return new FileResponseDto { FileName = file.FileName, DocumentContentType = file.ContentType, DocumentStream = file.Stream };
            }
            catch
            {
                return null;
            }
        }

        public async Task<GetOrganizationMessagesResponseDto> GetGetOrganizationMessagesAsync(GetOrganizationMessagesRequestDto request)
        {
            return await GetAsync<GetOrganizationMessagesResponseDto>("/CaseBookApi/V3/GetOrganizationMessages", request).ConfigureAwait(false);
        }

        public async Task<GetOrganizationInfoResponseDto> GetOrganizationInfoAsync(GetOrganizationInfoRequestDto request)
        {
            return await PostAsync<GetOrganizationInfoRequestDto, GetOrganizationInfoResponseDto>("/CaseBookApi/V3/GetOrganizationInfo", request).ConfigureAwait(false);
        }

        public async Task<PledgesResponseDto> GetPledgesMessagesAsync(GetPledgesRequestDto request)
        {
            return await GetAsync<PledgesResponseDto>("/CaseBookApi/V3/GetPledges", request, setting: _pledgeSettings).ConfigureAwait(false);
        }

        public async Task<GetExecutoryMessagesResponseDto> GetExecutoryMessagesAsync(GetExecutoryMessagesRequestDto request)
        {
            return await GetAsync<GetExecutoryMessagesResponseDto>("/CaseBookApi/V3/GetExecutoryMessages", request).ConfigureAwait(false);
        }
    }
}