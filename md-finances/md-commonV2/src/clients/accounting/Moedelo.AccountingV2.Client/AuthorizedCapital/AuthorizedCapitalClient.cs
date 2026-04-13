using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.AuthorizedCapital;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.AuthorizedCapital
{
    [InjectAsSingleton]
    public class AuthorizedCapitalClient : BaseApiClient, IAuthorizedCapitalClient
    {
        private readonly SettingValue apiEndPoint;
        
        public AuthorizedCapitalClient(
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

        public Task<AuthorizedCapitalDto> GetAsync(int firmId, int userId)
        {
            return GetAsync<AuthorizedCapitalDto>("/AuthorizedCapital", new {firmId, userId});
        }

        public Task CreateAsync(int firmId, int userId, AuthorizedCapitalDto authorizedCapital)
        {
            return PostAsync($"/AuthorizedCapital?firmId={firmId}&userId={userId}", authorizedCapital);
        }

        public Task DeleteAsync(int firmId, int userId)
        {
            return DeleteAsync($"/AuthorizedCapital?firmId={firmId}&userId={userId}");
        }
    }
}