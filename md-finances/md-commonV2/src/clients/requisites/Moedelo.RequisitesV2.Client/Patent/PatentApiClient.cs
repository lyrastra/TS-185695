using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RequisitesV2.Dto.Patent;

namespace Moedelo.RequisitesV2.Client.Patent
{
    [InjectAsSingleton]
    public class PatentApiClient : BaseApiClient, IPatentApiClient
    {
        private readonly SettingValue apiEndPoint;

        public PatentApiClient(
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
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<List<PatentDto>> GetAllAsync(int firmId, int userId, int? year)
        {
            var result = await GetAsync<GetPatentListResponseDto>("/Patent/GetAllV2", new { firmId, userId, year }).ConfigureAwait(false);
            return result.Items;
        }

        public async Task<PatentDto> GetAsync(int firmId, int userId, long patentId)
        {
            var result = await GetAsync<DataWrapper<PatentDto>>(
                "/Patent/Get",
                new
                {
                    firmId,
                    userId,
                    id = patentId
                }).ConfigureAwait(false);

            return result.Data;
        }

        public async Task<List<PatentDto>> GetByIdsAsync(int firmId, int userId, IReadOnlyCollection<long> patentIds)
        {
            var result = await PostAsync<IReadOnlyCollection<long>, ListWrapper<PatentDto>>(
                $"/Patent/GetByIds?firmId={firmId}&userId={userId}",
                patentIds).ConfigureAwait(false);

            return result.Items;
        }

        public Task<PatentDto> GetByWizardIdAsync(int firmId, int userId, long wizardId)
        {
            return GetAsync<PatentDto>("/Patent/GetByWizardId",
                new
                {
                    firmId,
                    userId,
                    wizardId
                });
        }

        public async Task<List<CodeKindOfBusinessDto>> GetAllCodeKindOfBusinessAsync(int firmId, int userId)
        {
            return await GetAsync<List<CodeKindOfBusinessDto>>("/Patent/GetAllCodeKindOfBusiness", new { firmId, userId }).ConfigureAwait(false);
        }

        public async Task<List<BudgetaryPatentAutocompleteDto>> BudgetaryPatentAutocompleteAsync(int firmId, int userId, int count, string query, DateTime? documentDate)
        {
            var result = await GetAsync<ListWrapper<BudgetaryPatentAutocompleteDto>>(
                "/Patent/BudgetaryPatentAutocomplete", new
                {
                    firmId = firmId,
                    userId = userId,
                    count = count,
                    query = query,
                    documentDate = documentDate
                }).ConfigureAwait(false);

            return result.Items;
        }

        public async Task<bool> IsExistsAsync(int firmId, int userId, long patentId)
        {
            var result = await GetAsync<DataWrapper<bool>>(
                "/Patent/IsExists",
                new
                {
                    firmId,
                    userId,
                    id = patentId
                }).ConfigureAwait(false);

            return result.Data;
        }

        public async Task<bool> IsAnyExistsAsync(int firmId, int userId, int year)
        {
            var result = await GetAsync<DataWrapper<bool>>(
                "/Patent/IsAnyExists",
                new
                {
                    firmId,
                    userId,
                    year = year
                }).ConfigureAwait(false);

            return result.Data;
        }

        public async Task<List<PatentDto>> GetWithoutAdditionalDataAsync(int firmId, int userId, int? year)
        {
            var result = await GetAsync<ListWrapper<PatentDto>>("/Patent/GetWithoutAdditionalData", new { firmId, userId, year }).ConfigureAwait(false);
            return result.Items;
        }

        public async Task<PatentDto> GetWithoutAdditionalDataByIdAsync(int firmId, int userId, long patentId)
        {
            var result = await GetAsync<DataWrapper<PatentDto>>("/Patent/GetWithoutAdditionalDataById", new { firmId, userId, id = patentId }).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<long> SaveAsync(int firmId, int userId, PatentDto dto)
        {
            var result = await PostAsync<PatentDto, DataWrapper<long>>($"/Patent/Save?firmId={firmId}&userId={userId}", dto);
            return result.Data;
        }
    }
}
