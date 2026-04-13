using System;
using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.AdvanceStatements;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Infrastructure.System.Extensions.Collections;

namespace Moedelo.Accounting.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IAdvanceStatementApiClient))]
    internal sealed class AdvanceStatementApiClient : BaseLegacyApiClient, IAdvanceStatementApiClient
    {
        public AdvanceStatementApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<AdvanceStatementApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
        }

        public Task<AdvanceStatementDto> GetByBaseIdAsync(FirmId firmId, UserId userId, long documentBaseId)
        {
            var uri = $"/AdvanceStatements/GetByBaseId?firmId={firmId}&userId={userId}&documentBaseId={documentBaseId}";
            return GetAsync<AdvanceStatementDto>(uri);
        }

        public Task<AdvanceStatementDto[]> GetByBaseIdsAsync(FirmId firmId, UserId userId,
            IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds == null || documentBaseIds.Count == 0)
            {
                return Task.FromResult(Array.Empty<AdvanceStatementDto>());
            }
            
            var uri = $"/AdvanceStatements/GetByBaseIds?firmId={firmId}&userId={userId}";
            return PostAsync<IReadOnlyCollection<long>, AdvanceStatementDto[]>(uri,
                documentBaseIds.ToDistinctReadOnlyCollection());
        }
    }
}