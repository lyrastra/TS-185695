using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RptV2.Dto.AutoWizardCompletion;

namespace Moedelo.RptV2.Client.AutoWizardCompletion
{
    [InjectAsSingleton]
    public class AutoPaySettingsApi : BaseApiClient, IAutoPaySettingsApi
    {
        private readonly HttpQuerySetting defaultSetting = new HttpQuerySetting(TimeSpan.FromMinutes(2));
        private readonly SettingValue apiEndpoint;
        
        public AutoPaySettingsApi(
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

        public async Task<IList<AutoWizardCompletionParamsDto>> GetAutoWizardCompletionParamsAsync()
        {
            return await GetAsync<IList<AutoWizardCompletionParamsDto>>("/AutoPaySettingsApi/GetAutoWizardCompletionParams", 
                setting: defaultSetting).ConfigureAwait(false);
        }

        public async Task<IList<AutoWizardCompletionParamsDto>> GetAutoWizardCompletionParamsForPeriodAsync(int forLastDays)
        {
            return await GetAsync<IList<AutoWizardCompletionParamsDto>>($"/AutoPaySettingsApi/GetAutoWizardCompletionParams?forLastDays={forLastDays}", 
                setting: defaultSetting).ConfigureAwait(false);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }
    }
}
