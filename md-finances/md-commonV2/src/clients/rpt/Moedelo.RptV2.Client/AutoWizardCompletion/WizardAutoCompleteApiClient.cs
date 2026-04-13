using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RptV2.Dto.WizardAutoCompletion;

namespace Moedelo.RptV2.Client.AutoWizardCompletion
{
    [InjectAsSingleton]
    public class WizardAutoCompleteApiClient : BaseApiClient, IWizardAutoCompleteApiClient
    {
        private readonly SettingValue apiEndpoint;
        private readonly HttpQuerySetting defaultSetting = new HttpQuerySetting(TimeSpan.FromMinutes(2));
        private readonly HttpQuerySetting autoCompleteQuerySetting = new HttpQuerySetting(TimeSpan.FromMinutes(5));

        public WizardAutoCompleteApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) 
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("RptApiEndpoint");
        }
        
        public async Task<AutoUsnResponse> CompleteUsnAdvanceWizardAsync(int firmId, int userId, int year, int period)
        {
            return await GetAsync<AutoUsnResponse>("/WizardAutoComplete/CompleteUsnWizard", 
                new { firmId, userId, year, period }, setting: defaultSetting).ConfigureAwait(false);
        }

        public async Task<AutoPfrResponse> CompletePfrWizardAsync(int firmId, int userId, int year, int period)
        {
            return await GetAsync<AutoPfrResponse>("/WizardAutoComplete/CompletePfrWizard", 
                new { firmId, userId, year, period }, setting: defaultSetting).ConfigureAwait(false);
        }
        
        public async Task<AutoCompleteWizardResponseDto> CompleteSzvmWizardAsync(int firmId, int userId,
            AutoCompleteWizardRequestDto request)
        {
            return await PostAsync<AutoCompleteWizardRequestDto, AutoCompleteWizardResponseDto>(
                $"/WizardAutoComplete/CompleteSzvmWizard?firmId={firmId}&userId={userId}", request,
                setting: autoCompleteQuerySetting).ConfigureAwait(false);
        }

        public async Task<AutoCompleteWizardResponseDto> CompleteRsvWizardAsync(int firmId, int userId, AutoCompleteWizardRequestDto request)
        {
            return await PostAsync<AutoCompleteWizardRequestDto, AutoCompleteWizardResponseDto>(
                $"/WizardAutoComplete/CompleteRsvWizard?firmId={firmId}&userId={userId}", request,
                setting: autoCompleteQuerySetting).ConfigureAwait(false);
        }

        public async Task<bool> CheckUserForUsnAsync(int firmId, int userId, int year)
        {
            return await GetAsync<bool>("/WizardAutoComplete/CheckUserForUsn", 
                new { firmId, userId, year }).ConfigureAwait(false);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }
    }
}