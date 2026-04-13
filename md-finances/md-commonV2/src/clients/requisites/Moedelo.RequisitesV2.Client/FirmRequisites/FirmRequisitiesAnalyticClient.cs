using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RequisitesV2.Dto.FirmRequisites;

namespace Moedelo.RequisitesV2.Client.FirmRequisites
{
    [InjectAsSingleton]
    public class FirmRequisitiesAnalyticClient : BaseApiClient, IFirmRequisitiesAnalyticClient
    {
        private readonly SettingValue apiEndPoint;
        
        public FirmRequisitiesAnalyticClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<List<FirmRequisitiesAnalyticDto>> GetFirmsRequisitiesAsync(List<int> firmIds)
        {
            return PostAsync<List<int>, List<FirmRequisitiesAnalyticDto>>("/Analytic/FirmsRequisities", firmIds);
        }
    }
}