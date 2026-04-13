using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PayrollV2.Client.Kontragents.Dto;

namespace Moedelo.PayrollV2.Client.Kontragents
{
    [InjectAsSingleton]
    public class KontragentsMergeApiClient : BaseApiClient, IKontragentsMergeApiClient
    {
        private readonly SettingValue apiEndPoint;

        public KontragentsMergeApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("PayrollPrivateApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/KontragentsMerge";
        }

        public Task ReplaceKontragentsInSalaryObjectsAsync(int firmId, int userId, KontragentReplaceDto request)
        {
            return PostAsync($"/ReplaceKontragentsInSalaryObjects?firmId={firmId}&userId={userId}", request);
        }
    }
}