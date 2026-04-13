using System;
using System.Threading.Tasks;
using Moedelo.Docs.Dto.PurchaseUpd.Rest;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Docs.Client.PurchasesUpd
{
    [InjectAsSingleton]
    public class PurchaseUpdRestApiClient : BaseApiClient, IPurchasesUpdRestApiClient
    {
        private readonly SettingValue apiEndpoint;

        public PurchaseUpdRestApiClient(
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
        public Task<PurchaseUpdDto> GetByBaseIdAsync(int firmId, int userId, long documentBaseId)
        {
            return GetAsync<PurchaseUpdDto>($"/api/v1/Purchases/Upd/{documentBaseId}?firmId={firmId}&userId={userId}");
        }

        /// <inheritdoc />
        public Task<PurchaseUpdDto> CreateAsync(int firmId, int userId, PurchaseUpdSaveRequestDto dto)
        {
            if (dto.Id > 0)
            {
                throw new ArgumentException("Saving of existent upd is not implemented. Waiting for UpdateAsync");
            }

            return PostAsync<PurchaseUpdSaveRequestDto, PurchaseUpdDto>($"/api/v1/Purchases/Upd?firmId={firmId}&userId={userId}", dto);
        }

        /// <inheritdoc />
        public Task DeleteAsync(int firmId, int userId, long documentBaseId)
        {
            return DeleteAsync($"/api/v1/Purchases/Upd/{documentBaseId}firmId={firmId}&userId={userId}");
        }
    }
}