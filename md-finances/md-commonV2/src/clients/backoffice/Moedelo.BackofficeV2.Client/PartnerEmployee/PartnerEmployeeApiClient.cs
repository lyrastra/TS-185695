using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.BackofficeV2.Dto.PartnerEmployee;

namespace Moedelo.BackofficeV2.Client.PartnerEmployee
{
    [InjectAsSingleton(typeof(IPartnerEmployeeApiClient))]
    public class PartnerEmployeeApiClient : BaseApiClient, IPartnerEmployeeApiClient
    {
        private readonly SettingValue apiEndPoint;

        public PartnerEmployeeApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("BackOfficePrivateApiEndpoint");
        }
        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
        public Task<PartnerEmployeeDto> GetByUserIdAsync(int userId)
        {
            return GetAsync<PartnerEmployeeDto>($"/Rest/PartnerEmployee/GetByUserId?userId={userId}");
        }
    }
}
