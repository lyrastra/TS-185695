using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.Salary.Dto;

namespace Moedelo.Salary.Client
{
    [InjectAsSingleton]
    public class Ndfl6ReportClient : BaseCoreApiClient, INdfl6ReportClient
    {
        private readonly ISettingRepository settingRepository;
        private const string uri = "/api/v1/Report";

        public Ndfl6ReportClient(
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
            return settingRepository.Get("SalaryNdfl6ReportApiEndpoint").Value;
        }

        public async Task<ReportChangedDto> InCompleteEventByQuickActionAsync(int firmId, int userId, 
            int eventId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var result =
                await PostAsync<ApiDataResult<ReportChangedDto>>(
                    $"{uri}/InCompleteEventByQuickAction/{eventId}",
                    tokenHeaders
                ).ConfigureAwait(false);

            return result?.data;
        }

        public async Task<ReportChangedDto> CompleteEventByQuickActionAsync(int firmId, int userId, 
            int eventId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var result =
                await PostAsync<ApiDataResult<ReportChangedDto>>(
                    $"{uri}/CompleteEventByQuickAction/{eventId}",
                    tokenHeaders
                ).ConfigureAwait(false);

            return result?.data;
        }
    }
}
