using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PriceLists.Dto.PriceLists;

namespace Moedelo.PriceLists.Client.PriceLists
{
    [InjectAsSingleton]
    public class PriceListsClient : BaseApiClient, IPriceListsClient
    {
        private readonly SettingValue apiEndpoint;
        
        public PriceListsClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("PriceListsApiEndpoint");
        }

        public Task<PriceListDto> GetByIdAsync(int id)
        {
            return GetAsync<PriceListDto>("/GetById", new {id});
        }

        public Task<List<PriceListDto>> GetAllAsync()
        {
            return GetAsync<List<PriceListDto>>("/GetAll");
        }

        public Task<List<PriceListDto>> GetActualAsync()
        {
            return GetAsync<List<PriceListDto>>("/Actual");
        }

        public Task<List<PriceListDto>> GetByTariffIdAsync(int tariffId)
        {
            return GetAsync<List<PriceListDto>>("/GetByTariffId", new {tariffId});
        }

        public Task<List<PriceListDto>> GetByTariffIdsAsync(List<int> tariffIds)
        {
            return PostAsync<List<int>, List <PriceListDto>>("/GetByTariffIds", tariffIds);
        }

        public Task<List<PriceListDto>> GetListForPartnerAsync()
        {
            return GetAsync<List<PriceListDto>>("/GetListForPartner");
        }
        
        public Task<List<PriceListDto>> GetAvailableForPartnerRegistrationAsync()
        {
            return GetAsync<List<PriceListDto>>("/GetAvailableForPartnerRegistration");
        }

        public Task<List<PriceListDto>> GetPagedListByNamePatternAsync(string namePattern, int offset, int size)
        {
            return GetAsync<List<PriceListDto>>("/GetPagedListByNamePattern", new {namePattern, offset, size});
        }

        public Task<int> CreateAsync(PriceListDto model)
        {
            return PostAsync<PriceListDto, int>("/Create", model);
        }

        public Task UpdateAsync(PriceListDto model)
        {
            return PostAsync("/Update", model);
        }

        public Task<bool> DeleteIfNotUsingAsync(DeletePriceListDto model)
        {
            return PostAsync<DeletePriceListDto, bool>("/DeleteIfNotUsing", model);
        }

        public Task<List<PriceListPositionDto>> GetPriceListPositionsAsync(int priceListId)
        {
            return GetAsync<List<PriceListPositionDto>>("/Positions", new { priceListId });
        }

        public Task<List<PriceListDto>> GetByIdListAsync(IReadOnlyCollection<int> priceListIds)
        {
            if (!priceListIds.Any())
            {
                return Task.FromResult(new List<PriceListDto>());
            }
            return PostAsync<IEnumerable<int>, List<PriceListDto>>("/GetByIdList", priceListIds.Distinct());
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/PriceLists";
        }
    }
}