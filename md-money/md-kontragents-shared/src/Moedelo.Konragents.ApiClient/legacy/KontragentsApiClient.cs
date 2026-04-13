using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Infrastructure.System.Extensions.Collections;
using Moedelo.Konragents.ApiClient.Abstractions.legacy;
using Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Konragents.ApiClient.legacy.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Konragents.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IKontragentsApiClient))]
    internal sealed class KontragentsApiClient : BaseLegacyApiClient, IKontragentsApiClient
    {
        public KontragentsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<KontragentsApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("KontragentsPrivateApiEndpoint"),
                logger)
        {
        }

        public Task<KontragentDto[]> GetAsync(FirmId firmId, UserId userId, HttpQuerySetting setting = null)
        {
            return GetAsync<KontragentDto[]>($"/V2/Get?firmId={firmId}&userId={userId}", setting: setting);
        }
        
        public async Task<int[]> GetIdsAsync(FirmId firmId, UserId userId, HttpQuerySetting setting = null)
        { 
            var result = await GetAsync<DataResult<int[]>>($"/GetIds?firmId={firmId}&userId={userId}", setting: setting);
            return result.Data;
        }

        public async Task<KontragentDto[]> GetByIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<int> ids)
        {
            if (ids?.Any() != true)
            {
                return Array.Empty<KontragentDto>();
            }

            var result = await PostAsync<IReadOnlyCollection<int>, DataResult<KontragentDto[]>>(
                    $"/Kontragents/GetByIds?firmId={firmId}&userId={userId}", ids.ToDistinctReadOnlyCollection())
                .ConfigureAwait(false);

            return result.Data;
        }

        public async Task<KontragentDto[]> GetByInnsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<string> inns)
        {
            if (inns?.Any() != true)
            {
                return Array.Empty<KontragentDto>();
            }

            var innsRequestString = string.Join(',',inns);
            var result = await GetAsync<DataResult<KontragentDto[]>>(
                $"/Kontragents/GetByInns?firmId={firmId}&userId={userId}&inns={innsRequestString}")
                    .ConfigureAwait(false);

            return result.Data;
        }

        public Task<KontragentDto> GetByInnsFromOfficeAsync(FirmId firmId, UserId userId, string inn)
        {
            if (string.IsNullOrEmpty(inn))
            {
                return Task.FromResult(new KontragentDto());
            }

            return GetAsync<KontragentDto>($"/FromOffice?firmId={firmId}&userId={userId}&inn={inn}");
        }

        public Task<List<BasicKontragentInfoDto>> GetBasicInfoByIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<int> ids)
        {
            if (ids?.Any() != true)
            {
                return Task.FromResult(new List<BasicKontragentInfoDto>());
            }

            return PostAsync<IReadOnlyCollection<int>, List<BasicKontragentInfoDto>>($"/Kontragents/GetBasicInfoByIds?firmId={firmId}&userId={userId}", ids);
        }

        public Task<List<BasicKontragentInfoDto>> GetBasicInfoByIdsWithoutContextAsync(IReadOnlyCollection<int> ids)
        {
            if (ids?.Any() != true)
            {
                return Task.FromResult(new List<BasicKontragentInfoDto>());
            }

            return PostAsync<IReadOnlyCollection<int>, List<BasicKontragentInfoDto>>($"/Kontragents/GetBasicInfoByIds", ids);
        }

        public Task<List<KontragentInfoForErptDto>> GetByIdsForErptAsync(IReadOnlyCollection<int> ids)
        {
            if (ids?.Any() != true)
            {
                return Task.FromResult(new List<KontragentInfoForErptDto>());
            }

            return PostAsync<IEnumerable<int>, List<KontragentInfoForErptDto>>($"/Kontragents/GetByIdsForErpt", ids);
        }

        public Task<List<KontragentInfoForErptDto>> GetAllForErptAsync(FirmId firmId, UserId userId)
        {
            return GetAsync<List<KontragentInfoForErptDto>>($"/Kontragents/GetAllForErpt?firmId={firmId}&userId={userId}");
        }

        public Task<int> SaveAsync(FirmId firmId, UserId userId, KontragentDto kontragentDto)
        {
            return PostAsync<KontragentDto, int>($"/V2/Save?firmId={firmId}&userId={userId}", kontragentDto);
        }

        public Task<KontragentSaveResultDto> CreateOrUpdateAsync(FirmId firmId, UserId userId, KontragentDto kontragentDto)
        {
            return PostAsync<KontragentDto, KontragentSaveResultDto>($"/V2/CreateOrUpdate?firmId={firmId}&userId={userId}", kontragentDto);
        }

        public async Task<int> CreatePopulationKontragentIfNotExistAsync(int firmId, int userId)
        {
            var dataDto = await PostAsync<DataResult<int>>($"/Kontragents/CreatePopulationKontragentIfNotExist?firmId={firmId}&userId={userId}").ConfigureAwait(false);
            return dataDto.Data;
        }

        public async Task<List<KontragentDto>> GetByNamesAsync(int firmId, int userId, ICollection<string> names)
        {
            var response = await GetAsync<DataResult<List<KontragentDto>>>("/GetByNames", new { firmId, userId, names = string.Join(",", names) }).ConfigureAwait(false);
            return response.Data;
        }

        public Task<List<KontragentDto>> GetByPurseIdsAsync(int firmId, int userId, IReadOnlyCollection<int> purseIds)
        {
            if (purseIds?.Any() != true)
            {
                return Task.FromResult(new List<KontragentDto>());
            }

            return PostAsync<IReadOnlyCollection<int>, List<KontragentDto>>
                ($"/V2/GetByPurseIds?firmId={firmId}&userId={userId}", purseIds.ToDistinctReadOnlyCollection());
        }

        public Task<bool> CanDeleteAsync(FirmId firmId, UserId userId, int id)
        {
            return GetAsync<bool>($"/V2/CanDelete?firmId={(int)firmId}&userId={(int)userId}&id={id}");
        }

        public Task DeleteAsync(FirmId firmId, UserId userId, int id)
        {
            return PostAsync($"/V2/Delete?firmId={(int)firmId}&userId={(int)userId}&id={id}");
        }

        public async Task<int> GetOrCreatePopulationAsync(FirmId firmId, UserId userId)
        {
            var result = await PostAsync<DataResult<int>>($"/Kontragents/CreatePopulationKontragentIfNotExist?firmId={(int)firmId}&userId={(int)userId}");
            return result.Data;
        }

        public async Task<KontragentDto> GetYourselfAsync(FirmId firmId, UserId userId)
        {
            var response =
                await GetAsync<DataResult<KontragentDto>>("/Kontragents/Yourself",
                        new { firmId, userId })
                    .ConfigureAwait(false);
            return response?.Data;
        }
        
        public Task<KontragentsPageDto> GetPageAsync(int firmId, int userId, KontragentsPageRequestDto request)
        {
            return PostAsync<KontragentsPageRequestDto, KontragentsPageDto>($"/V2/GetPage?firmId={firmId}&userId={userId}", request);
        }
    }
}
