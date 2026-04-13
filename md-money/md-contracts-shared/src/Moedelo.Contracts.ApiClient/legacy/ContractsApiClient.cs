using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Dto;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Contracts.ApiClient.Abstractions.legacy;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Contracts.ApiClient.legacy.models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Contracts.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IContractsApiClient))]
    internal sealed class ContractsApiClient : BaseLegacyApiClient, IContractsApiClient
    {
        public ContractsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<ContractsApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("ContractApiEndpoint"),
                logger
            )
        {
        }

        public async Task<List<ContractDto>> GetByBaseIdsAsync(FirmId firmId, UserId userId,
            IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return new List<ContractDto>();
            }

            var uri = $"/GetByBaseIds?firmId={firmId}&userId={userId}";
            var result = await PostAsync<IReadOnlyCollection<long>, ListResult<ContractDto>>(uri, baseIds)
                .ConfigureAwait(false);
            
            return result.Items;
        }

        public async Task<ContractDto> GetOrCreateMainContractAsync(FirmId firmId, UserId userId, int kontragentId)
        {
            var uri = $"/GetOrCreateMainContract?firmId={firmId}&userId={userId}&kontragentId={kontragentId}";
            var result = await GetAsync<DataResult<ContractDto>>(uri).ConfigureAwait(false);
            
            return result.Data;
        }

        public async Task<List<ContractDto>> GetByIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<int> ids)
        {
            if (ids?.Any() != true)
            {
                return new List<ContractDto>();
            }

            var uri = $"/GetByIds?firmId={firmId}&userId={userId}";
            var result = await PostAsync<IReadOnlyCollection<int>, ListResult<ContractDto>>(uri, ids)
                .ConfigureAwait(false);

            return result.Items;
        }

        public async Task<RentalContractDto> GetRentalByIdAsync(FirmId firmId, UserId userId, int id)
        {
            var uri = $"/GetRentalById?id={id}&firmId={firmId}&userId={userId}";
            var result = await GetAsync<DataResult<RentalContractDto>>(uri).ConfigureAwait(false);
            
            return result.Data;
        }

        public async Task<List<RentalPaymentItemDto>> GetRentalPaymentItemsByIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<int> ids)
        {
            if (ids?.Any() != true)
            {
                return new List<RentalPaymentItemDto>();
            }

            var uri = $"/GetRentalPaymentItemsByIds?firmId={firmId}&userId={userId}";
            var result = await PostAsync<IReadOnlyCollection<int>, ListResult<RentalPaymentItemDto>>(uri, ids)
                .ConfigureAwait(false);

            return result.Items;
        }

        public Task<List<ContractSearchResponseDto>> SearchAsync(FirmId firmId, UserId userId, ContractSearchCriteriaDto request, bool useReadOnly)
        {
            return PostAsync<ContractSearchCriteriaDto, List<ContractSearchResponseDto>>(
                $"/Search?firmId={firmId}&userId={userId}&useReadOnly={useReadOnly}", request);
        }

        public Task<ApiDataDto<int>> SaveAsync(FirmId firmId, UserId userId, ContractDto contract)
        {
            return PostAsync<ContractDto, ApiDataDto<int>>($"/Save?firmId={firmId}&userId={userId}", contract);
        }

        public Task<ContractSavedDto> SaveV2Async(FirmId firmId, UserId userId, ContractV1Dto contractDto)
        {
            return PostAsync<ContractV1Dto, ContractSavedDto>($"/V2/Save?firmId={firmId}&userId={userId}", contractDto);
        }
    }
}