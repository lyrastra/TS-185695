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
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Requisites.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(ITaxationSystemApiClient))]
    internal sealed class TaxationSystemApiClient : BaseLegacyApiClient, ITaxationSystemApiClient
    {
        public TaxationSystemApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<TaxationSystemApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("FirmRequisitesApiEndpoint"),
                logger)
        {
        }

        public Task<TaxationSystemDto[]> GetAsync(FirmId firmId, UserId userId)
        {
            var uri = $"/TaxationSystem?firmId={firmId}&userId={userId}";
            return GetAsync<TaxationSystemDto[]>(uri);
        }

        public Task<IReadOnlyCollection<FirmTaxationSystemDto>> GetAsync(IReadOnlyCollection<int> firmIds)
        {
            var uri = $"/TaxationSystem/GetByFirmIds";
            return PostAsync<IReadOnlyCollection<int>, IReadOnlyCollection<FirmTaxationSystemDto>>(uri, firmIds);
        }

        public Task<TaxationSystemDto> GetByYearAsync(FirmId firmId, UserId userId, int year)
        {
            var uri = $"/TaxationSystem/{year}?firmId={firmId}&userId={userId}";
            return GetAsync<TaxationSystemDto>(uri);
        }

        public async Task<ActualFirmTaxationSystemDto> GetActualAsync(
            int firmId,
            CancellationToken cancelationToken = default)
        {
            var result = await GetActualAsync([firmId], cancelationToken);
            return result?.FirstOrDefault();
        }

        public Task<IReadOnlyCollection<ActualFirmTaxationSystemDto>> GetActualAsync(
            IReadOnlyCollection<int> firmIds,
            CancellationToken cancellationToken,
            HttpQuerySetting setting = null)
        {
            return PostAsync<IReadOnlyCollection<int>, IReadOnlyCollection<ActualFirmTaxationSystemDto>>(
                "/TaxationSystem/Actual",
                firmIds,
                cancellationToken: cancellationToken,
                setting: setting);
        }

        public Task SaveAsync(FirmId firmId, UserId userId, TaxationSystemDto taxationSystem)
        {
            var uri = $"/TaxationSystem?firmId={firmId}&userId={userId}";
            return PostAsync(uri, taxationSystem);
        }
    }
}