using System;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.System.Extensions.Collections;

namespace Moedelo.Requisites.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IPatentEventAttributeApiClient))]
    internal sealed class PatentEventAttributeApiClient : BaseLegacyApiClient, IPatentEventAttributeApiClient
    {
        public PatentEventAttributeApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PatentEventAttributeApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("FirmRequisitesApiEndpoint"),
                logger)
        {
        }

        public Task<PatentEventAttributeDto[]> GetByPatentIdsAsync(FirmId firmId, UserId userId,
            IReadOnlyCollection<long> patentIds)
        {
            if (patentIds?.Any() != true)
            {
                return Task.FromResult(Array.Empty<PatentEventAttributeDto>());
            }

            var uri = $"/PatentEventAttribute/ByPatentIds?firmId={firmId}&userId={userId}";
            
            return PostAsync<IReadOnlyCollection<long>, PatentEventAttributeDto[]>(uri,
                patentIds.ToDistinctReadOnlyCollection());
        }
    }
}