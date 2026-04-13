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
    public class EmploymentHistoryStatementClient : BaseCoreApiClient, IEmploymentHistoryStatementClient
    {
        private readonly ISettingRepository settingRepository;
        private const string uri = "/api/v1/EmploymentHistoryStatement";

        public EmploymentHistoryStatementClient(
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

        public async Task<EmploymentHistoryStatementDto> GetByWorkerAsync(
            int firmId,
            int userId,
            int workerId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var path = $"{uri}/{workerId}"; //потому что BaseCoreApiClient формирует ури как /api/v1/EmploymentHistoryStatement/?workerId=1, а нужно /api/v1/EmploymentHistoryStatement/1

            var response = await GetAsync<ApiDataResult<EmploymentHistoryStatementDto>>(
                path,
                queryHeaders: tokenHeaders).ConfigureAwait(false);

            return response.data;
        }

        public async Task<int> InsertAsync(
            int firmId,
            int userId,
            EmploymentHistoryStatementDto model)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var response = await PostAsync<EmploymentHistoryStatementDto, ApiDataResult<int>>(
                uri,
                model,
                queryHeaders: tokenHeaders).ConfigureAwait(false);

            return response.data;
        }

        public async Task UpdateAsync(
            int firmId,
            int userId,
            EmploymentHistoryStatementDto model)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var path = $"{uri}/{model.Id}"; //потому что BaseCoreApiClient формирует ури как /api/v1/EmploymentHistoryStatement/, а нужно /api/v1/EmploymentHistoryStatement/1

            await PutAsync(
                path,
                model,
                queryHeaders: tokenHeaders).ConfigureAwait(false);
        }
    }
}
