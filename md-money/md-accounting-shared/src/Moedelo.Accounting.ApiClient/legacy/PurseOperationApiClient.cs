using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.System.Extensions.Collections;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.PurseOperation;

namespace Moedelo.Accounting.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IPurseOperationApiClient))]
    internal sealed class PurseOperationApiClient : BaseLegacyApiClient, IPurseOperationApiClient
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

        public Task DeleteAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds == null || baseIds.Count == 0)
            {
                return Task.CompletedTask;
            }
            
            var uri = $"/PurseOperationApi/Delete?firmId={firmId}&userId={userId}";
            return DeleteByRequestAsync(uri, baseIds.ToDistinctReadOnlyCollection());
        }

        public Task SavePurseOperationWithTypeAsync(FirmId firmId, UserId userId, PurseOperationForMultipleTypesDto dto)
        {
            var uri = $"/PurseOperationApi/SavePurseOperationWithType?firmId={firmId}&userId={userId}";
            return PostAsync(uri, dto);
        }

        public Task ChangeTaxationSystemAsync(FirmId firmId, UserId userId, ChangeTaxationSystemRequestDto dto)
        {
            var uri = $"/PurseOperation/ChangeTaxationSystem?firmId={firmId}&userId={userId}";
            return PostAsync(uri, dto);
        }
    }
}