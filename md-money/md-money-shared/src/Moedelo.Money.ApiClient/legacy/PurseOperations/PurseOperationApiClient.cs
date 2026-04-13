using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.System.Extensions.Collections;
using Moedelo.Money.ApiClient.Abstractions.legacy.PurseOperations;
using Moedelo.Money.ApiClient.Abstractions.legacy.PurseOperations.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.ApiClient.legacy.PurseOperations
{
    [InjectAsSingleton(typeof(IPurseOperationApiClient))]
    sealed class PurseOperationApiClient : BaseLegacyApiClient, IPurseOperationApiClient
    {
        public PurseOperationApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PurseOperationApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
        }

        public Task<ReadPurseOperationDto[]> GetByBaseIdsAsync(FirmId firmId, UserId userId,
            IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(Array.Empty<ReadPurseOperationDto>());
            }

            var uri = $"/PurseOperationApi/GetByBaseIds?firmId={firmId}&userId={userId}";
            return PostAsync<IReadOnlyCollection<long>, ReadPurseOperationDto[]>(uri,
                baseIds.ToDistinctReadOnlyCollection());
        }

        public Task<CreatedPurseOperationDto> SavePurseOperationWithTypeAsync(int firmId, int userId, PurseOperationDto dto)
        {
            var uri = $"/PurseOperationApi/SavePurseOperationWithType?firmId={firmId}&userId={userId}";
            return PostAsync<PurseOperationDto, CreatedPurseOperationDto>(uri, dto);
        }

        public Task<CreatedPurseOperationDto> SaveRefundToClientAsync(int firmId, int userId, PurseOperationDto dto)
        {
            var uri = $"/PurseOperationApi/SaveRefundToClient?firmId={firmId}&userId={userId}";
            return PostAsync<PurseOperationDto, CreatedPurseOperationDto>(uri, dto);
        }
    }
}
