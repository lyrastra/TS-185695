using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.Salary.Dto;
using Moedelo.Salary.Dto.EmploymentHistoryReportInfo;

namespace Moedelo.Salary.Client
{
    [InjectAsSingleton]
    public class EmploymentHistoryReportClient : BaseCoreApiClient, IEmploymentHistoryReportClient
    {
        private readonly ISettingRepository settingRepository;
        private const string uri = "/api/v1/Report";
        private const string uriReportInfo = "/api/v1/ReportInfo";

        public EmploymentHistoryReportClient(
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
            this.settingRepository = settingRepository;
        }

        protected override string GetApiEndpoint()
        {
            return settingRepository.Get("SalaryEmploymentHistoryApiEndpoint").Value;
        }

        public async Task<ReportChangedDto> InCompleteByQuickActionAsync(int firmId, int userId, 
            int eventId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var result =
                await GetAsync<ApiDataResult<ReportChangedDto>>($"{uri}/InCompleteByQuickAction",
                    new {firmId, userId, eventId}, tokenHeaders).ConfigureAwait(false);

            return result?.data;
        }

        public async Task<ReportChangedDto> CompleteByQuickActionAsync(int firmId, int userId, 
            int eventId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var result =
                await GetAsync<ApiDataResult<ReportChangedDto>>($"{uri}/CompleteByQuickAction",
                    new {firmId, userId, eventId}, tokenHeaders).ConfigureAwait(false);
            
            return result?.data;
        }

        public async Task<List<EmptyReportDto>> GetEmptyReportsAsync(
            int firmId, int userId,
            int[] firmIds, int[] calendarIds)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            
            var result =
                await PostAsync<GetEmptyReportRequestDto, ApiDataResult<List<EmptyReportDto>>>(
                    $"{uriReportInfo}/GetEmptyReports", new GetEmptyReportRequestDto
                    {
                        CalendarIds = calendarIds,
                        FirmIds = firmIds
                    },
                    tokenHeaders
                ).ConfigureAwait(false);

            return result?.data;
        }

        public async Task<int?> GetFinishedReportWithWorkerAsync(
            int firmId, int userId,
            int workerId, EmploymentChangingType? eventType = null)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var result =
                await GetAsync<ApiDataResult<int?>>($"{uriReportInfo}/GetFinishedReportWithWorker",
                    new { workerId, eventType }, tokenHeaders).ConfigureAwait(false);

            return result.data;
        }

        public async Task DeleteReportByUserEventAsync(int firmId, int userId, int userEventId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            await DeleteAsync($"{uri}/ByUserEvent/{userEventId}", queryHeaders: tokenHeaders).ConfigureAwait(false);
        }
        
        public async Task<AutoCompleteWizardResponseDto> AutoCompleteWizardAsync(int firmId, int userId, 
            AutoCompleteWizardRequestDto request)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var result = await PostAsync<AutoCompleteWizardRequestDto, ApiDataResult<AutoCompleteWizardResponseDto>>(
                $"{uri}/AutoComplete", request, tokenHeaders).ConfigureAwait(false);

            return result?.data;
        }
    }
}
