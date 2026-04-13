using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.RetailReports;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.RetailReports.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Exceptions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.legacy.Sales.RetailReports
{
    [InjectAsSingleton(typeof(IRetailReportApiClient))]
    public class RetailReportApiClient : BaseLegacyApiClient, IRetailReportApiClient
    {
        public RetailReportApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<RetailReportApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
        }

        public Task<List<RetailReportDto>> GetByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<RetailReportDto>());
            }

            return PostAsync<IReadOnlyCollection<long>, List<RetailReportDto>>(
                $"/RetailReportApi/GetByBaseIds?firmId={firmId}&userId={userId}", baseIds);
        }

        public Task<RetailReportDto> GetByBaseIdAsync(FirmId firmId, UserId userId, long baseId)
        {
            try
            {
                return GetAsync<RetailReportDto>(
                    $"/RetailReportApi/GetByBaseIdAsync?firmId={firmId}&userId={userId}&baseId={baseId}");
            }
            catch (HttpRequestResponseStatusException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                throw;
            }
        }
    }
}