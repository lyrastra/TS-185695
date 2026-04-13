using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto.Dto;
using Moedelo.AccPostings.Enums;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Infrastructure.System.Extensions.Collections;

namespace Moedelo.AccPostings.ApiClient.legacy.Subconto
{
    [InjectAsSingleton(typeof(ISubcontoClient))]
    internal sealed class SubcontoClient : BaseLegacyApiClient, ISubcontoClient
    {
        public SubcontoClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SubcontoClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccPostingsApiEndpoint"),
                logger)
        {
        }

        public Task<List<SubcontoDto>> GetByIdsAsync(
            FirmId firmId, UserId userId, IReadOnlyCollection<long> ids, bool isFromReadonlyDb = false)
        {
            if (ids.NullOrEmpty())
            {
                return Task.FromResult(new List<SubcontoDto>());
            }

            var url = $"/Subconto/Get?firmId={firmId}&userId={userId}&isFromReadonlyDb={isFromReadonlyDb}";

            return PostAsync<IReadOnlyCollection<long>, List<SubcontoDto>>(url, ids.ToDistinctReadOnlyCollection());
        }

        public Task<List<CostItemGroupDto>> GetCostItemGroupsAsync(FirmId firmId, UserId userId)
        {
            var url = $"/Subconto/CostItemGroup?firmId={firmId}&userId={userId}";

            return GetAsync<List<CostItemGroupDto>>(url);
        }

        public Task<SubcontoDto> GetDefaultSubcontoAsync(FirmId firmId, UserId userId, SubcontoType type)
        {
            var url = $"/Subconto/Default?firmId={firmId}&userId={userId}&type={type}";
            
            return GetAsync<SubcontoDto>(url);
        }

        public Task<List<NomenclatureGroupDto>> GetNomenclatureGroupsAsync(FirmId firmId, UserId userId)
        {
            var url = $"/Subconto/NomenclatureGroup?firmId={firmId}&userId={userId}";
            
            return GetAsync<List<NomenclatureGroupDto>>(url);
        }

        public Task<SubcontoDto> GetOrCreateTextSubcontoAsync(FirmId firmId, UserId userId, SubcontoType type, string name)
        {
            var url = $"/Subconto/GetOrCreateTextSubconto?firmId={firmId}&userId={userId}";
            var dto = new CreateTextSubcontoDto { Name = name, Type = type };

            return PostAsync<CreateTextSubcontoDto, SubcontoDto>(url, dto);
        }

        public Task<long> SaveAsync(int firmId, int userId, SubcontoDto subconto)
        {
            if (subconto == null)
            {
                throw new ArgumentNullException(nameof(subconto));
            }

            var url = $"/Subconto/Save?firmId={firmId}&userId={userId}";

            return PostAsync<SubcontoDto, long>(url, subconto);
        }

        public Task DeleteAsync(int firmId, int userId, long id)
        {
            var url = $"/Subconto/{id}?firmId={firmId}&userId={userId}";
            
            return DeleteAsync(url);
        }

        public Task<List<NdsRateDto>> GetNdsRatesAsync(FirmId firmId, UserId userId)
        {
            var url = $"/Subconto/NdsRate?firmId={firmId}&userId={userId}";

            return GetAsync<List<NdsRateDto>>(url);
        }

        public Task<SubcontoDto[]> GetByTypeAutocompleteAsync(FirmId firmId, UserId userId, SubcontoType type, string query = "", int count = 5)
        {
            var url = $"/Subconto/GetByTypeAutocomplete?firmId={firmId}&userId={userId}&type={type}&query={query}&count={count}";
            
            return GetAsync<SubcontoDto[]>(url);
        }
        
        private class CreateTextSubcontoDto
        {
            public string Name { get; set; }
            
            public SubcontoType Type { get; set; }
        }
    }
}