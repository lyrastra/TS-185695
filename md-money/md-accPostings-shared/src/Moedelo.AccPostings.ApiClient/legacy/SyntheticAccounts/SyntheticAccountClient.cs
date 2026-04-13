using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.SyntheticAccounts;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.SyntheticAccounts.Dto;
using Moedelo.AccPostings.Enums;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.System.Extensions.Collections;

namespace Moedelo.AccPostings.ApiClient.legacy.SyntheticAccounts
{
    [InjectAsSingleton(typeof(ISyntheticAccountClient))]
    internal sealed class SyntheticAccountClient : BaseLegacyApiClient, ISyntheticAccountClient
    {
        public SyntheticAccountClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SyntheticAccountClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccPostingsApiEndpoint"),
                logger)
        {
        }

        public Task<SyntheticAccountDto[]> GetByIds(IReadOnlyCollection<long> ids)
        {
            if (ids.NullOrEmpty())
            {
                return Task.FromResult(Array.Empty<SyntheticAccountDto>());
            }

            return PostAsync<IReadOnlyCollection<long>, SyntheticAccountDto[]>(
                "/SyntheticAccount/ByIds",
                ids.ToDistinctReadOnlyCollection());
        }

        public Task<SyntheticAccountDto[]> GetByCodes(IReadOnlyCollection<SyntheticAccountCode> codes)
        {
            if (codes.NullOrEmpty())
            {
                return Task.FromResult(Array.Empty<SyntheticAccountDto>());
            }

            return PostAsync<IReadOnlyCollection<SyntheticAccountCode>, SyntheticAccountDto[]>(
                "/SyntheticAccount/ByCodes",
                codes.ToDistinctReadOnlyCollection());
        }

        public Task<SyntheticAccountDto[]> GetActualAsync()
        {
            return GetAsync<SyntheticAccountDto[]>("/SyntheticAccount/Actual");
        }

        public Task<SyntheticAccountSubcontoRuleDto[]> GetRulesByCodesAsync(IReadOnlyCollection<SyntheticAccountCode> codes)
        {
            if (codes.NullOrEmpty())
            {
                return Task.FromResult(Array.Empty<SyntheticAccountSubcontoRuleDto>());
            }

            return PostAsync<IReadOnlyCollection<SyntheticAccountCode>, SyntheticAccountSubcontoRuleDto[]>(
                "/SyntheticAccount/RulesByCodes",
                codes.ToDistinctReadOnlyCollection());
        }
    }
}