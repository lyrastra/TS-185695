using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.LeadSource;
using Moedelo.Common.Enums.Enums.Partner;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CatalogV2.Client.LeadSource
{
    [InjectAsSingleton]
    public class LeadSourceApiClient : BaseApiClient, ILeadSourceApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public LeadSourceApiClient(
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
            apiEndPoint = settingRepository.Get("CatalogApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/LeadSource/V2";
        }

        public Task<List<LeadSourceDto>> GetListAsync(RegionalPartnerType? partnerType)
        {
            return GetAsync<List<LeadSourceDto>>("/GetList", new {partnerType});
        }

        public Task<int> SaveOrUpdateAsync(LeadSourceDto leadSourceDto)
        {
            return PostAsync<LeadSourceDto, int>("/SaveOrUpdate", leadSourceDto);
        }

        public Task DeleteAsync(int id)
        {
            return PostAsync($"/Delete?id={id}");
        }
    }
}