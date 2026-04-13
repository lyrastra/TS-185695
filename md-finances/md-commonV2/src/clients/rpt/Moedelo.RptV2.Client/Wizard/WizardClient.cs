using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Reports;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RptV2.Dto;
using Moedelo.RptV2.Dto.Wizard;

namespace Moedelo.RptV2.Client.Wizard
{
    [InjectAsSingleton]
    public class WizardClient : BaseApiClient, IWizardClient
    {
        private readonly SettingValue apiEndpoint;

        public WizardClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,  
            ISettingRepository settingRepository) : base(
                httpRequestExecutor, 
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("RptApiEndpoint");
        }

        public async Task<WizardStatusResultDto> CompleteByCalendarEventIdAsync(int firmId, int userId, int eventId, WizardTypeClientString type)
        {
            return (await PostAsync<DataWrapper<WizardStatusResultDto>>($"/CompleteByCalendarEventId?firmId={firmId}&userId={userId}&eventId={eventId}&type={(int)type}").ConfigureAwait(false)).Data;
        }

        public Task<List<ProfitDeclarationDto>> GetCompletedProfitDeclarations(int firmId, int userId)
        {
            return GetAsync<List<ProfitDeclarationDto>>("/GetCompletedProfitDeclarations", new { firmId, userId });
        }

        public Task<PayerRequisitesDto> GetPayerRequisites(int firmId, int userId, int wizardId, int paymentIndex )
        {
            return GetAsync<PayerRequisitesDto>("/GetPayerRequisites", new { firmId, userId, wizardId, paymentIndex });
        }

        public async Task<WizardStatusResultDto> InCompleteByCalendarEventIdAsync(int firmId, int userId, int eventId, WizardTypeClientString type)
        {
            return (await PostAsync<DataWrapper<WizardStatusResultDto>>($"/IncompleteByCalendarEventId?firmId={firmId}&userId={userId}&eventId={eventId}&type={(int)type}").ConfigureAwait(false)).Data;
        }

        public async Task<WizardStatusResultDto> IncompleteTradingTaxPaymentFromRequisitesAsync(int firmId, int userId,
            int wizardStateId)
        {
            var url = $"/IncompleteTradingTaxPaymentFromRequisites?firmId={firmId}&userId={userId}&wizardStateId={wizardStateId}";

            return (await PostAsync<DataWrapper<WizardStatusResultDto>>(url).ConfigureAwait(false)).Data;
        }

        public Task<bool> CheckWizardIsCompleteAsync(int firmId, int userId, long wizardId)
        {
            return GetAsync<bool>("/CheckWizardIsComplete", new { firmId, userId, wizardId });
        }

        public Task RemoveWizardAsync(int firmId, int userId, long wizardId)
        {
            return GetAsync("/RemoveWizard", new { firmId, userId, wizardId });
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/Wizard";
        }

        public Task<int> GetWizardYearAsync(int firmId, int userId, long wizardId)
        {
            return GetAsync<int>("/GetWizardYear", new { firmId, userId, wizardId });
        }

        public Task<IReadOnlyCollection<WizardStateDto>> FindWizardStateByYearAndTypeAsync(
            FindWizardStateByYearAndTypeRequestDto request)
        {
            return PostAsync<FindWizardStateByYearAndTypeRequestDto, IReadOnlyCollection<WizardStateDto>>("/FindWizardStateByYearAndType", request);
        }
    }
}
