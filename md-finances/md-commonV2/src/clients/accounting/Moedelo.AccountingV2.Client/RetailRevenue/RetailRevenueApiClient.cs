using System.Threading.Tasks;
using Moedelo.AccountingV2.Client.RetailRevenue.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.RetailRevenue
{
    [InjectAsSingleton]
    public class RetailRevenueApiClient : BaseApiClient, IRetailRevenueApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public RetailRevenueApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<RetailRevenueDto> GetByBaseIdAsync(int firmId, int userId, long cashierId, long baseId)
        {
            return GetAsync<RetailRevenueDto>($"/RetailRevenueApi/GetByBaseId?firmId={firmId}&userId={userId}&cashierId={cashierId}&baseId={baseId}");
        }

        public async Task<RetailRevenueDto> GetAsync(int firmId, int userId, long cashierId, long id)
        {
            var response = await GetAsync<DataResponseWrapper<RetailRevenueDto>>($"/RetailRevenueApi/Get?firmId={firmId}&userId={userId}&cashierId={cashierId}&id={id}").ConfigureAwait(false);
            return response.Data;
        }

        public async Task<RetailRevenueDto> CreateAsync(int firmId, int userId, long cashierId, RetailRevenueDto dto)
        {
            var response = await PostAsync<RetailRevenueDto, DataResponseWrapper<RetailRevenueDto>>($"/RetailRevenueApi/CreateAsync?firmId={firmId}&userId={userId}&cashierId={cashierId}", dto).ConfigureAwait(false);
            return response.Data;
        }

        public async Task<RetailRevenueCollectionDto> GetListAsync(int firmId, int userId, CashierPaginationCriterions cashierPaginationRequest)
        {
            var response = await PostAsync<CashierPaginationCriterions, DataResponseWrapper<RetailRevenueCollectionDto>>($"/RetailRevenueApi/GetListAsync?firmId={firmId}&userId={userId}", cashierPaginationRequest).ConfigureAwait(false);
            return response.Data;
        }

        public async Task<bool> ExistsCashOrderAsync(int firmId, int userId, long cashierId, long baseId)
        {
            var response = await GetAsync<DataResponseWrapper<bool>>($"/RetailRevenueApi/ExistsCashOrder?firmId={firmId}&userId={userId}&cashierId={cashierId}&baseId={baseId}").ConfigureAwait(false);
            return response.Data;
        }

        public async Task<RetailRevenueDto> UpdateAsync(int firmId, int userId, long cashierId, RetailRevenueDto dto)
        {
            var response = await PostAsync<RetailRevenueDto, DataResponseWrapper<RetailRevenueDto>>($"/RetailRevenueApi/UpdateAsync?firmId={firmId}&userId={userId}&cashierId={cashierId}", dto)
                .ConfigureAwait(false);

            return response.Data;
        }

        public Task DeleteAsync(int firmId, int userId, long cashierId, long baseId)
        {
            return PostAsync($"/RetailRevenueApi/Delete?firmId={firmId}&userId={userId}&cashierId={cashierId}&baseId={baseId}");
        }
    }
}