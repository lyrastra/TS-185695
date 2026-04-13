using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PayrollV2.Client.MdPulse.DTO;

namespace Moedelo.PayrollV2.Client.MdPulse
{
    [InjectAsSingleton]
    public class MdPulseApiClient : BaseApiClient, IMdPulseApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public MdPulseApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager
                 )
        {
            apiEndPoint = settingRepository.Get("PayrollPrivateApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/MdPulse";
        }

        public Task<List<FirmMrotStatusDto>> GetFirmsMrotStatusAsync(IEnumerable<int> firmIds, IEnumerable<string> regionCodes = null)
        {
            return PostAsync<MdPulseRequest, List<FirmMrotStatusDto>>("/GetFirmsMrotStatus", new MdPulseRequest { FirmIds = firmIds, RegionCodes = regionCodes });
        }
    }
}