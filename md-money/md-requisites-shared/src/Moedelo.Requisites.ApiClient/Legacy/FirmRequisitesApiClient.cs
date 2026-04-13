using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Requisites.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IFirmRequisitesApiClient))]
    internal sealed class FirmRequisitesApiClient : BaseLegacyApiClient, IFirmRequisitesApiClient
    {
        public FirmRequisitesApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<FirmRequisitesApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("FirmRequisitesApiEndpoint"),
                logger)
        {
        }

        public Task<FirmRequisitesDto> GetAsync(FirmId firmId)
        {
            var uri = $"/FirmRequisites/GetFirmById?id={firmId}";
            return GetAsync<FirmRequisitesDto>(uri);
        }

        public Task<IReadOnlyCollection<FirmRequisitesDto>> GetFirmsByIdsAsync(IReadOnlyCollection<int> firmIds)
        {
            var uri = "/FirmRequisites/GetFirmsByIds";
            return PostAsync<IReadOnlyCollection<int>, IReadOnlyCollection<FirmRequisitesDto>>(uri, firmIds);
        }

        public Task<RegistrationDataDto> GetRegistrationDataAsync(FirmId firmId, UserId userId)
        {
            var uri = $"/Firm/RegistrationData?firmId={firmId}&userId={userId}";
            return GetAsync<RegistrationDataDto>(uri);
        }

        public Task<IReadOnlyCollection<FirmRegistrationDataDto>> GetRegistrationDataAsync(
            IReadOnlyCollection<int> firmIds,
            CancellationToken cancellationToken)
        {
            if (firmIds?.Any() != true)
            {
                return Task.FromResult<IReadOnlyCollection<FirmRegistrationDataDto>>(Array.Empty<FirmRegistrationDataDto>());
            }

            var uri = $"/Firm/GetRegistrationDataForFirms";
            return PostAsync<IReadOnlyCollection<int>, IReadOnlyCollection<FirmRegistrationDataDto>>(
                uri,
                firmIds,
                cancellationToken: cancellationToken);
        }

        public Task<RegistrationShortDataDto[]> GetRegistrationShortDataAsync(
            IReadOnlyCollection<int> firmIds)
        {
            if (firmIds?.Any() != true)
            {
                return Task.FromResult(Array.Empty<RegistrationShortDataDto>());
            }

            var uri = $"/Firm/RegistrationShortDataByIds";
            return PostAsync<IReadOnlyCollection<int>, RegistrationShortDataDto[]>(uri, firmIds);
        }

        public Task<RegistrationShortDataDto[]> GetOsnoRegistrationShortDataAsync(int year)
        {
            var uri = $"/Firm/OsnoRegistrationShortData?year={year}";
            return GetAsync<RegistrationShortDataDto[]>(uri);
        }

        public Task<DirectorDto> GetDirectorAsync(FirmId firmId, UserId userId)
        {
            var uri = $"/Firm/Director?firmId={firmId}&userId={userId}";
            return GetAsync<DirectorDto>(uri);
        }

        public async Task<IReadOnlyCollection<int>> GetFirmIdsByInn(string inn)
        {
            var uri = $"/Firm/GetFirmIdsByInn?inn={inn}";
            var ids = await GetAsync<int[]>(uri).ConfigureAwait(false);

            return ids.ToDistinctReadOnlyCollection();
        }

        public Task SaveRegistrationDataAsync(FirmId firmId, UserId userId, RegistrationDataDto data)
        {
            var uri = $"/Firm/RegistrationData?firmId={firmId}&userId={userId}";
            return PostAsync(uri, data);
        }

        public Task SetEmployerModeAsync(FirmId firmId, UserId userId, bool isEmployerMode)
        {
            var uri = $"/FirmRequisites/SetEmployerMode?firmId={firmId}&userId={userId}&isEmployerMode={isEmployerMode}";
            return PostAsync(uri);
        }

        public Task SaveDirectorAsync(FirmId firmId, UserId userId, DirectorDto director)
        {
            var uri = $"/Firm/Director?firmId={firmId}&userId={userId}";
            return PostAsync(uri, director);
        }

        public Task SetDirectorAsync(FirmId firmId, UserId userId, int directorId)
        {
            return PostAsync($"/Firm/Director/SetDirector?firmId={firmId}&userId={userId}&directorId={directorId}");
        }

        public Task SetInFaceAsync(FirmId firmId, UserId userId, DirectorSetInFaceDto dto)
        {
            return PostAsync($"/Firm/Director/SetInFace?firmId={firmId}&userId={userId}", dto);
        }

        public Task FillByInnAsync(FirmId firmId, UserId userId, string inn, bool withDirector = true, CancellationToken? cancellationToken = null)
        {
            var settings = new HttpQuerySetting(TimeSpan.FromSeconds(90));
            var uri = $"/Firm/RegistrationData/FillByInn?firmId={firmId}&userId={userId}&inn={inn}&withDirector={withDirector}";
            return PostAsync(uri, setting: settings, cancellationToken: cancellationToken ?? CancellationToken.None);
        }

        public Task<FirmShortInfoDto[]> GetFirmShortInfosAsync(IReadOnlyCollection<int> firmIds)
        {
            if (firmIds?.Any() != true)
            {
                return Task.FromResult(Array.Empty<FirmShortInfoDto>());
            }
            const string uri = $"/FirmRequisites/GetFirmShortInfos";
            return PostAsync<IReadOnlyCollection<int>, FirmShortInfoDto[]>(uri, firmIds);
        }

        public Task<IReadOnlyList<FirmShortInfoDto>> GetFirmShortInfosRegisteredOnAsync(DateTime date,
            CancellationToken cancellationToken)
        {
            var uri = $"/FirmRequisites/GetFirmShortInfosRegisteredOn?date={date:yyyy-MM-dd}";
            return GetAsync<IReadOnlyList<FirmShortInfoDto>>(uri, cancellationToken: cancellationToken);
        }

        public Task<FirmShortInfoWithoutPhoneDto[]> GetFirmShortInfosWithoutPhoneAsync(IReadOnlyCollection<int> firmIds,
            CancellationToken cancellationToken)
        {
            if (firmIds?.Any() != true)
            {
                return Task.FromResult(Array.Empty<FirmShortInfoWithoutPhoneDto>());
            }

            const string uri = "/FirmRequisites/GetFirmShortInfosWithoutPhone";

            return PostAsync<IReadOnlyCollection<int>, FirmShortInfoWithoutPhoneDto[]>(uri, firmIds, cancellationToken: cancellationToken);
        }

        public Task SavePassportDataAsync(FirmId firmId, UserId userId, PassportDataDto data)
        {
            return PostAsync($"/Firm/PassportData?firmId={firmId}&userId={userId}", data);
        }

        public Task<IReadOnlyDictionary<int, int?>> GetRegionIdsByFirmIdsAsync(IReadOnlyCollection<int> firmIds)
        {
            return PostAsync<IReadOnlyCollection<int>, IReadOnlyDictionary<int, int?>>(
                $"/Firm/GetRegionIdsByFirmIds",
                firmIds);
        }

        public Task<PassportDataDto> GetPassportDataAsync(FirmId firmId, UserId userId)
        {
            return GetAsync<PassportDataDto>($"/Firm/PassportData?firmId={firmId}&userId={userId}");
        }

        public Task<List<DirectorBirthdayDataDto>> GetDirectorBirthdayDataAsync(IReadOnlyCollection<int> firmIds, CancellationToken cancellationToken = default)
        {

            var settings = new HttpQuerySetting(TimeSpan.FromSeconds(90));
            var uri = "/FirmRequisites/GetDirectorBirthdayData";


            return PostAsync<IReadOnlyCollection<int>, List<DirectorBirthdayDataDto>>(uri, firmIds.Distinct().ToList(), cancellationToken: cancellationToken, setting: settings); ;
        }
        
        public Task<RequisitesForFormDto> GetRequisitesForFormByFirmId(int firmId)
        {
            return GetAsync<RequisitesForFormDto>("/FirmRequisites/GetRequisitesForFormByFirmId", new { firmId });
        }
    }
}