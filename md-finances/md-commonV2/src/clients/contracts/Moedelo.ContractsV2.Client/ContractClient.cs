using Moedelo.Common.Enums.Enums.Contract;
using Moedelo.ContractsV2.Client.DtoWrappers;
using Moedelo.ContractsV2.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.ContractsV2.Client
{
    [InjectAsSingleton]
    public class ContractClient : BaseApiClient, IContractClient
    {
        private readonly SettingValue apiEndpoint;

        public ContractClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor,
                uriCreator,
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ContractApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public async Task<ContractDto> GetByIdAsync(int firmId, int userId, int id)
        {
            return (await GetByIdsAsync(firmId, userId, new[] { id }).ConfigureAwait(false)).FirstOrDefault();
        }

        public async Task<List<ContractDto>> GetByIdsAsync(int firmId, int userId, IReadOnlyCollection<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return new List<ContractDto>();
            }

            var result = await PostAsync<IReadOnlyCollection<int>, ListDto<ContractDto>>(
                $"/GetByIds?firmId={firmId}&userId={userId}",
                ids).ConfigureAwait(false);

            return result.Items;
        }

        public async Task<List<ContractDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds == null || !baseIds.Any())
            {
                return new List<ContractDto>();
            }

            var result = await PostAsync<IReadOnlyCollection<long>, ListDto<ContractDto>>(
                $"/GetByBaseIds?firmId={firmId}&userId={userId}",
                baseIds).ConfigureAwait(false);

            return result.Items;
        }

        public async Task<List<ContractDto>> GetByNumbersAsync(int firmId, int userId,
            IReadOnlyCollection<string> numbers)
        {
            if (numbers == null || !numbers.Any())
            {
                return new List<ContractDto>();
            }

            var result = await PostAsync<IReadOnlyCollection<string>, ListDto<ContractDto>>(
                $"/GetByNumbers?firmId={firmId}&userId={userId}",
                numbers).ConfigureAwait(false);

            return result.Items;
        }

        public Task<List<ContractDto>> GetByNumberAsync(int firmId, int userId, string number)
        {
            return GetByNumbersAsync(firmId, userId, new[] { number });
        }

        public async Task<ContractDto> GetOrCreateMainContractAsync(int firmId, int userId, int kontragentId)
        {
            var result = await GetAsync<DataDto<ContractDto>>(
                "/GetOrCreateMainContract",
                new { firmId, userId, kontragentId }).ConfigureAwait(false);

            return result.Data;
        }
        
        public async Task<List<ContractDto>> GeMainContractsByKontragentIdsAsync(int firmId, int userId, IReadOnlyCollection<int> kontragentIds)
        {
            if (kontragentIds?.Any() != true)
            {
                return new List<ContractDto>(0);
            }
            
            var result = await PostAsync<IReadOnlyCollection<int>, ListDto<ContractDto>>(
                $"/GeMainContractsByKontragentIds?firmId={firmId}&userId={userId}",
                kontragentIds).ConfigureAwait(false);

            return result.Items;
        }

        public async Task<List<ContractAutocompleteItemDto>> GetAutocompleteAsync(int firmId, int userId, string query = "",
            int? count = null, int? kontragentId = null)
        {
            var result = await GetAsync<ListDto<ContractAutocompleteItemDto>>(
                "/GetAutocomplete",
                new { firmId, userId, query, count, kontragentId }).ConfigureAwait(false);

            return result.Items;
        }

        public async Task<List<ContractAutocompleteItemDto>> GetAutocompleteAsync(int firmId, int userId, string query = "",
            int? count = null, int? kontragentId = null, bool onlyFounders = false)
        {
            var result = await GetAsync<ListDto<ContractAutocompleteItemDto>>(
                "/GetAutocomplete",
                new { firmId, userId, query, count, kontragentId, onlyFounders }).ConfigureAwait(false);

            return result.Items;
        }

        public async Task<List<ContractFileDto>> GetContractFilesByKontragentIdsAsync(
            int firmId,
            int userId,
            IReadOnlyCollection<int> kontragentIds)
        {
            if (kontragentIds?.Any() != true)
            {
                return new List<ContractFileDto>();
            }

            var result = await PostAsync<IReadOnlyCollection<int>, ListDto<ContractFileDto>>(
                $"/GetContractFilesByKontragentIds?firmId={firmId}&userId={userId}",
                kontragentIds).ConfigureAwait(false);

            return result.Items;
        }

        public async Task<List<ContractDto>> GetBySubcontoIdsAsync(int firmId, int userId, IReadOnlyCollection<long> subcontoIds)
        {
            if (subcontoIds == null || !subcontoIds.Any())
            {
                return new List<ContractDto>();
            }

            var result = await PostAsync<IReadOnlyCollection<long>, ListDto<ContractDto>>(
                $"/GetBySubcontoIds?firmId={firmId}&userId={userId}",
                subcontoIds).ConfigureAwait(false);

            return result.Items;
        }

        public Task<ContractSavedDto> SaveAync(int firmId, int userId, ContractV1Dto dto)
        {
            var uri = $"/V2/Save?firmId={firmId}&userId={userId}";
            return PostAsync<ContractV1Dto, ContractSavedDto>(uri, dto);
        }

        public async Task<List<ContractV1Dto>> GetListAsync(int firmId, int userId, string query, int offset, int count, int? kontragentId = null)
        {
            var result = await GetAsync<ListDto<ContractV1Dto>>("/GetList",
                new { firmId, userId, query, offset, count, kontragentId }).ConfigureAwait(false);

            return result.Items;
        }

        public async Task<bool> CanDeleteMainContractAsync(int firmId, int userId, int kontragentId)
        {
            var result = await GetAsync<DataDto<bool>>(
                "/CanDeleteMainContract",
                new { firmId, userId, kontragentId }).ConfigureAwait(false);
            return result.Data;
        }

        public Task DeleteMainContractAsync(int firmId, int userId, long kontragentId)
        {
            return PostAsync($"/DeleteKontragentsMainContracts?firmId={firmId}&userId={userId}", new[] { kontragentId });
        }

        public Task DeleteMainContractsAsync(int firmId, int userId, IReadOnlyCollection<int> kontragentIds)
        {
            return PostAsync($"/DeleteKontragentsMainContracts?firmId={firmId}&userId={userId}", kontragentIds);
        }

        public async Task<int> GetCountByKontragentAsync(int firmId, int userId, long kontragentId)
        {
            var result = await GetAsync<DataDto<int>>("/GetCountByKontragent", new { firmId, userId, kontragentId })
                .ConfigureAwait(false);
            return result.Data;
        }

        public async Task<List<ContractsCountByKontragentDto>> GetCountByKontragentsAsync(int firmId, int userId, IReadOnlyCollection<int> kontragentIds)
        {
            var result = await PostAsync<IReadOnlyCollection<int>, ListDto<ContractsCountByKontragentDto>>(
                $"/GetCountByKontragents?firmId={firmId}&userId={userId}", kontragentIds).ConfigureAwait(false);
            return result.Items;
        }

        public Task ReplaceKontragentInContractsAsync(int firmId, int userId, ReplaceKontragentDto request, IReadOnlyCollection<KeyValuePair<string, string>> queryHeaders = null, HttpQuerySetting setting = null)
        {
            return PostAsync($"/ReplaceKontragentInContracts?firmId={firmId}&userId={userId}", request);
        }

        public async Task<RentalContractDto> GetRentalByIdAsync(int firmId, int userId, int id, HttpQuerySetting setting = null)
        {
            var result = await GetAsync<DataDto<RentalContractDto>>(
                $"/GetRentalById",
                new { firmId, userId, id },
                setting: setting).ConfigureAwait(false);

            return result.Data;
        }

        public Task<List<ContractSearchResultDto>> Search(int firmId, int userId, ContractSearchCriteriaDto criteriaDto)
        {
            return PostAsync<ContractSearchCriteriaDto, List<ContractSearchResultDto>>(
                $"/Search?firmId={firmId}&userId={userId}", criteriaDto);
        }

        public async Task<List<RentalContractDto>> GetRentalByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds == null || !baseIds.Any())
            {
                return new List<RentalContractDto>();
            }

            var result = await PostAsync<IReadOnlyCollection<long>, ListDto<RentalContractDto>>(
                $"/GetRentalByBaseIds?firmId={firmId}&userId={userId}",
                baseIds).ConfigureAwait(false);

            return result.Items;
        }

        public Task<List<RentalPaymentItemDto>> GetRentalPaymentItemsByContractBaseIds(int firmId, int userId, IReadOnlyCollection<long> contractBaseIds)
        {
            if (contractBaseIds?.Any() != true)
            {
                return Task.FromResult(new List<RentalPaymentItemDto>());
            }

            return PostAsync<IReadOnlyCollection<long>, List<RentalPaymentItemDto>>(
                $"/GetRentalPaymentItemsByContractBaseIds?firmId={firmId}&userId={userId}", contractBaseIds);
        }

        public Task<bool> AnyMediationContractsAsync(int firmId, int userId, MediationType type)
        {
            return GetAsync<bool>($"/IsMediationContractExists", new { firmId, userId, type });
        }

        public Task<TemplateFileDto> DownloadUserTemplateAsync(int firmId, int userId, int templateId)
        {
            return GetAsync<TemplateFileDto>($"/UserTemplateDownload?firmId={firmId}&userId={userId}&templateId={templateId}");
        }

        public Task UploadUserTemplateAsync(int firmId, int userId, TemplateFileDto file)
        {
            return PostAsync($"/UserTemplateUpload?firmId={firmId}&userId={userId}", file);
        }

        public Task<IReadOnlyCollection<UserTemplateDto>> GetUserTemplatesAsync(int firmId, int userId)
        {
            return GetAsync<IReadOnlyCollection<UserTemplateDto>>($"/UserTemplates?firmId={firmId}&userId={userId}");
        }
        
        public Task<HttpFileModel> GetTemplateFileForPrintAsync(int firmId, int userId, int contractId)
        {
            return GetFileAsync($"/TemplateFile/GetForPrint?firmId={firmId}&userId={userId}&contractId={contractId}");
        }
    }
}