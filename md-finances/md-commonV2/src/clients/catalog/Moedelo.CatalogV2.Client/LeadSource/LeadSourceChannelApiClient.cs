using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.LeadSource;
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
    public class LeadSourceChannelApiClient : BaseApiClient, ILeadSourceChannelApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public LeadSourceChannelApiClient(
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
            return apiEndPoint.Value + "/LeadSourceChannel/V2";
        }

        public Task<List<LeadSourceChannelDto>> GetListAsync()
        {
            return GetAsync<List<LeadSourceChannelDto>>("/GetList", null);
        }

        public Task<int> SaveOrUpdateAsync(LeadSourceChannelDto channelDto)
        {
            return PostAsync<LeadSourceChannelDto, int>("/SaveOrUpdate", channelDto);
        }

        public Task DeleteAsync(int id)
        {
            return PostAsync($"/Delete?id={id}");
        }
    }
}