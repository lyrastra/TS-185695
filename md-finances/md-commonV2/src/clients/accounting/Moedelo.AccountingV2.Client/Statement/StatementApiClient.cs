using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Statements;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.Statement
{
    [InjectAsSingleton]
    public class StatementApiClient : BaseApiClient, IStatementApiClient
    {
        private readonly SettingValue apiEndPoint;

        public StatementApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }


        public Task<SavedStatementDto> CreateAsync(int firmId, int userId, StatementDto dto)
        {
            return PostAsync<StatementDto, SavedStatementDto>($"/StatementApi/Create?firmId={firmId}&userId={userId}", dto);
        }

        public Task DeleteByBaseIdAsync(int firmId, int userId, long documentBaseId)
        {
            return PostAsync($"/StatementApi/Delete?firmId={firmId}&userId={userId}&documentBaseId={documentBaseId}");
        }
    }
}
