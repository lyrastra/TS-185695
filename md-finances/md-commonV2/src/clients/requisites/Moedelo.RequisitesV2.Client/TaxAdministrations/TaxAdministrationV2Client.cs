using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RequisitesV2.Dto.TaxAdministrations;

namespace Moedelo.RequisitesV2.Client.TaxAdministrations
{
    [InjectAsSingleton]
    public class TaxAdministrationV2Client : BaseApiClient, ITaxAdministrationV2Client
    {
        private readonly SettingValue apiEndPoint;
        
        public TaxAdministrationV2Client(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,  
            ISettingRepository settingRepository) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<TaxAdministrationV2Dto> GetForFirmAsync(int firmId, int userId)
        {
            return GetAsync<TaxAdministrationV2Dto>(
                "/TaxAdministration/GetTaxAdminDataByFirmId", 
                new { firmId, userId});
        }

        public Task<TaxAdministrationV2Dto> GetByCodeAsync(int firmId, int userId, string code)
        {
            return GetAsync<TaxAdministrationV2Dto>(
                "/TaxAdministration/GetByCode", new
                    { firmId, userId, code});
        }

        public Task<TaxAdministrationV2Dto> GetByIdAsync(int firmId, int userId, int id)
        {
            return GetAsync<TaxAdministrationV2Dto>(
                "/TaxAdministration/GetTaxAdministrationByIdAsync", new
                    { firmId, userId, id});
        }
    }
}