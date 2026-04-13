using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.AccountManagement.Client.OutsourcingMdUserRole
{
    [InjectAsSingleton]
    public class OutsourcingMdUserRoleApiClient : BaseApiClient, IOutsourcingMdUserRoleApiClient
    {
        private readonly SettingValue apiEndPoint;

        public OutsourcingMdUserRoleApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountManagementPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Rest/OutsourcingMdUserRole";
        }

        public async Task SetRoleToDirectorAsync(int masterUserId, int masterFirmId, IReadOnlyCollection<int> firmIds)
        {
            await PostAsync($"/SetRoleToDirector?userId={masterUserId}&firmId={masterFirmId}", firmIds).ConfigureAwait(false);
        }

        public async Task SetRoleToAdminAsync(int masterUserId, int masterFirmId, IReadOnlyCollection<int> firmIds)
        {
            await PostAsync($"/SetRoleToAdmin?userId={masterUserId}&firmId={masterFirmId}", firmIds).ConfigureAwait(false);
        }
    }
}