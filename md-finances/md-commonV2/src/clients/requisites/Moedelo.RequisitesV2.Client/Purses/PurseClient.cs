using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RequisitesV2.Dto.Purses;

namespace Moedelo.RequisitesV2.Client.Purses
{
    [InjectAsSingleton]
    public class PurseClient : BaseApiClient, IPurseClient
    {
        private readonly SettingValue apiEndPoint;

        public PurseClient(
            IHttpRequestExecutor requestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(requestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<List<PurseDto>> GetAsync(int firmId, int userId)
        {
            var result = await GetAsync<ListWrapper<PurseDto>>("/Purse/GetAll", new { firmId, userId }).ConfigureAwait(false);
            return result.Items;
        }

        public async Task<List<PurseDto>> GetByIdsAsync(int firmId, int userId, IReadOnlyCollection<int> ids)
        {
            var response = await PostAsync<IReadOnlyCollection<int>, ListWrapper<PurseDto>>(
                $"/Purse/GetByIds?firmId={firmId}&userId={userId}", ids).ConfigureAwait(false);
            return response.Items;
        }

        public async Task<PurseDto> GetByNameAsync(int firmId, int userId, string name)
        {
            var result = await GetAsync<DataWrapper<PurseDto>>("/Purse/GetByName", new { firmId, userId, name }).ConfigureAwait(false);
            return result.Data;
        }

        public Task UpdateNameAsync(int firmId, int userId, int purseId, string name)
        {
            return PostAsync<object>($"/Purse/UpdateName?firmId={firmId}&userId={userId}", new { purseId, name });
        }

        public async Task<int> SaveVirtualPurseAsync(int firmId, int userId, PurseDto purse)
        {
            var result = await PostAsync<PurseDto, DataWrapper<int>>(
                $"/Purse/SaveVirtualPurse?firmId={firmId}&userId={userId}", purse).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<List<int>> CanBeDeletedAsync(int firmId, int userId, IReadOnlyCollection<int> purseIds)
        {
            if (purseIds?.Any() != true)
            {
                return new List<int>();
            }
            
            var response = await PostAsync<PurseValidationForDeleteRequestDto, DataWrapper<List<int>>>(
                $"/Purse/CanBeDeleted?firmId={firmId}&userId={userId}", 
                new PurseValidationForDeleteRequestDto{ PurseIds = purseIds }).ConfigureAwait(false);
            
            return response.Data;
        }

        public Task DeleteByIdsAsync(int firmId, int userId, IReadOnlyCollection<int> purseIds, bool removeRelatedKontragents = false)
        {
            if (purseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }
            
            return PostAsync<PurseDeleteRequestDto, object>(
                $"/Purse/Delete?firmId={firmId}&userId={userId}", 
                new PurseDeleteRequestDto
                {
                    PurseIds = purseIds, 
                    RemoveRelatedKontragents = removeRelatedKontragents
                });
        }

        public async Task<List<BankSettlementPurseDto>> GetByRecipientSettlementsAsync(int firmId, int userId, IReadOnlyCollection<string> numbers)
        {
            var result = await PostAsync< IReadOnlyCollection<string>, DataWrapper<List<BankSettlementPurseDto>>>(
                $"/Purse/GetPursesByRecipientSettlements?firmId={firmId}&userId={userId}", numbers).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<int> SaveBankSettlementPurseAsync(int firmId, int userId, BankSettlementPurseDto purse)
        {
            var result = await PostAsync<BankSettlementPurseDto, DataWrapper<int>>(
                $"/Purse/SaveBankSettlementPurse?firmId={firmId}&userId={userId}", purse).ConfigureAwait(false);
            return result.Data;
        }
    }
}