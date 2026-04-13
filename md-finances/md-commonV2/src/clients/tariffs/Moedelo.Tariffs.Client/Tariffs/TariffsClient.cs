using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Tariffs.Dto.Tariffs;

namespace Moedelo.Tariffs.Client.Tariffs
{
    [InjectAsSingleton]
    public class TariffsClient : BaseApiClient, ITariffsClient
    {
        private readonly SettingValue apiEndpoint;
        
        public TariffsClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("TariffsApiEndpoint");
        }

        public Task<TariffDto> GetByIdAsync(int id)
        {
            return GetAsync<TariffDto>("/GetById", new {id});
        }

        public Task<List<TariffDto>> GetAsync(IReadOnlyCollection<int> tariffIds)
        {
            if (tariffIds?.Any() != true)
            {
                return Task.FromResult(new List<TariffDto>());
            }

            return PostAsync<IEnumerable<int>, List<TariffDto>>("/GetList", tariffIds.Distinct());
        }

        public Task<List<TariffDto>> GetAllAsync()
        {
            return GetAsync<List<TariffDto>>("/GetAll");
        }

        public Task<List<TariffDto>> GetPagedListByCriteriaAsync(string namePattern, bool? isWithAccess, bool? isOneTime, int offset, int size)
        {
            return GetAsync<List<TariffDto>>("/GetPagedListByCriteria",
                new {namePattern, isWithAccess, isOneTime, offset, size});
        }

        public Task<bool> IsLegacyLicenseActOnlyTariffAsync(int tariffId, DateTime paymentDate)
        {
            return GetAsync<bool>("/IsLegacyLicenseActOnlyTariff", new {tariffId, paymentDate});
        }

        public Task<List<TariffDto>> GetByAsync(TariffFilterDto requestDto)
        {
            return PostAsync<TariffFilterDto, List<TariffDto>>("/GetBy", requestDto);
        }

        public Task<List<TariffDto>> GetByPriceListIdListAsync(IReadOnlyCollection<int> priceListIdList)
        {
            return PostAsync<IReadOnlyCollection<int>, List<TariffDto>>("/GetByPriceListIdList", priceListIdList);
        }

        public async Task<IReadOnlyDictionary<int, TariffDto>> GetByPriceListIdsAsync(IReadOnlyCollection<int> priceListIds)
        {
            if (priceListIds?.Any() != true)
            {
                return new Dictionary<int, TariffDto>();
            }

            var response = await PostAsync<IEnumerable<int>, TariffsByPriceListIdsResponseDto>(
                "/GetByPriceListIds", priceListIds).ConfigureAwait(false);

            var tariffsMap = response.Tariffs.ToDictionary(t => t.Id, t => t);

            return response.TariffByPriceList.ToDictionary(
                p => p.Key,
                p => tariffsMap[p.Value]);
        }


        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/Tariffs";
        }
    }
}