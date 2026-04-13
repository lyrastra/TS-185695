using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.AccPostings.Dto;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccPostings.Client
{
    [InjectAsSingleton]
    public class SubcontoClient : BaseApiClient, ISubcontoClient
    {
        private readonly SettingValue apiEndPoint;

        public SubcontoClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccPostingsApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<List<SubcontoDto>> GetAsync(int firmId, int userId, IReadOnlyCollection<long> subcontoIds)
        {
            if (subcontoIds == null || !subcontoIds.Any())
            {
                return Task.FromResult(new List<SubcontoDto>());
            }

            return PostAsync<IReadOnlyCollection<long>, List<SubcontoDto>>(
                $"/Subconto/Get?firmId={firmId}&userId={userId}",
                subcontoIds);
        }

        public Task<long> SaveAsync(int firmId, int userId, SubcontoSaveRequestDto subconto)
        {
            if (subconto == null)
            {
                throw new ArgumentNullException(nameof(subconto));
            }

            return PostAsync<SubcontoSaveRequestDto, long>(
                $"/Subconto/Save?firmId={firmId}&userId={userId}",
                subconto);
        }

        public Task<SubcontoDto> GetDefaultSubcontoAsync(int firmId, int userId, SubcontoType type)
        {
            return GetAsync<SubcontoDto>("/Subconto/Default", new {firmId, userId, type});
        }


        public Task<List<SubcontoDto>> GetByTypeAsync(int firmId, int userId, SubcontoType type)
        {
            return GetAsync<List<SubcontoDto>>("/Subconto/GetByType", new {firmId, userId, type});
        }

        public Task<List<NomenclatureGroupDto>> GetNomenclatureGroupsAsync(int firmId, int userId)
        {
            return GetAsync<List<NomenclatureGroupDto>>("/Subconto/NomenclatureGroup", new {firmId, userId});
        }

        public Task<List<CostItemGroupDto>> GetCostItemGroupsAsync(int firmId, int userId)
        {
            return GetAsync<List<CostItemGroupDto>>("/Subconto/CostItemGroup", new {firmId, userId});
        }

        public Task<List<NdsRateDto>> GetNdsRatesAsync(int firmId, int userId)
        {
            return GetAsync<List<NdsRateDto>>("/Subconto/NdsRate", new {firmId, userId});
        }

        public Task DeleteAsync(int firmId, int userId, long id)
        {
            return DeleteAsync($"/Subconto/{id}", new {firmId, userId});
        }

        public Task DeleteAsync(int firmId, int userId, IReadOnlyCollection<long> ids)
        {
            if (ids?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return DeleteAsync($"/Subconto", new {firmId, userId, ids});
        }

        public Task<SubcontoDto> GetOrCreateTextSubcontoAsync(int firmId, int userId, SubcontoType type, string name)
        {
            return PostAsync<TextSubcontoRequestDto, SubcontoDto>(
                $"/Subconto/GetOrCreateTextSubconto?firmId={firmId}&userId={userId}",
                new TextSubcontoRequestDto
                {
                    Name = name,
                    Type = type
                });
        }

        public Task<List<SubcontoDto>> GetByTypeAutocompleteAsync(int firmId, int userId, SubcontoType type, 
            string query = "", int count = 5)
        {
            return GetAsync<List<SubcontoDto>>("/Subconto/GetByTypeAutocomplete",
                new {firmId, userId, type, query, count});
        }
    }
}