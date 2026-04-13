using System.Threading.Tasks;
using Moedelo.Docs.Dto.SalesUpd.Rest;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Docs.Client.SalesUpd
{
    [InjectAsSingleton]
    public class SalesUpdRestApiClient : BaseApiClient, ISalesUpdRestApiClient
    {
        private readonly SettingValue apiEndpoint;

        public SalesUpdRestApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,  
            ISettingRepository settingRepository) : 
            base(
                httpRequestExecutor, 
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("DocsApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        /// <inheritdoc />
        public Task<SalesUpdRestDto> GetByBaseIdAsync(int firmId, int userId, long documentBaseId)
        {
            return GetAsync<SalesUpdRestDto>($"/api/v1/sales/Upd/{documentBaseId}?firmId={firmId}&userId={userId}");
        }

        /// <inheritdoc />
        public Task<SalesUpdRestDto> CreateAsync(int firmId, int userId, SalesUpdCreateRequestRestDto dto)
        {
            return PostAsync<SalesUpdCreateRequestRestDto, SalesUpdRestDto>($"/api/v1/sales/Upd?firmId={firmId}&userId={userId}", dto);
        }

        /// <inheritdoc />
        public Task DeleteAsync(int firmId, int userId, long documentBaseId)
        {
            return DeleteAsync($"/api/v1/sales/Upd/{documentBaseId}firmId={firmId}&userId={userId}");
        }
    }
}