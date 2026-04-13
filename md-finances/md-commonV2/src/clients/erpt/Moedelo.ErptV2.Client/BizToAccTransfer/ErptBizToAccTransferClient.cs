using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.ErptV2.Client.BizToAccTransfer
{
    [InjectAsSingleton]
    public class ErptBizToAccTransferClient:BaseApiClient, IErptBizToAccTransferClient
    {
        private readonly SettingValue apiEndpoint;

        public ErptBizToAccTransferClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(httpRequestExecutor, uriCreator,
            responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ErptApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task RollbackEdsAsync(int fromFirmId, int toFirmId) =>
            PostAsync($"/BizToAccTransfer/RollbackEds?fromFirmId={fromFirmId}&toFirmId={toFirmId}");

    }
}
