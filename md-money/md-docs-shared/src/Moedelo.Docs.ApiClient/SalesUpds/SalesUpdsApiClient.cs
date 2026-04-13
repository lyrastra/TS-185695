using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Upds.Dto;
using Moedelo.Docs.ApiClient.Abstractions.SalesUpds;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.SalesUpds
{
    [InjectAsSingleton(typeof(ISalesUpdsApiClient))]
    public class SalesUpdsApiClient : BaseApiClient, ISalesUpdsApiClient 
    {
        public SalesUpdsApiClient(
            IHttpRequestExecuter httpRequestExecuter, 
            IUriCreator uriCreator, 
            IAuditTracer auditTracer, 
            IAuthHeadersGetter authHeadersGetter, 
            IAuditHeadersGetter auditHeadersGetter, 
            ISettingRepository settingRepository,
            ILogger<SalesUpdsApiClient> logger) 
            : base(
                httpRequestExecuter, 
                uriCreator, 
                auditTracer, 
                authHeadersGetter, 
                auditHeadersGetter, 
                settingRepository.Get("DocsApiEndpoint"), 
                logger)
        {
        }

        public async Task<long> GetNextNumberAsync(int firmId, int userId, int year)
        {
            var result = await GetAsync<DataResponse<DocumentNextNumberDto>>($"/api/v1/Sales/Upd/GetNextNumber/{year}", new
            {
                firmId,
                userId,
            });

            return result.Data.NextNumber;
        }
        
        public async Task<bool> IsDocumentNumberBusyAsync(int firmId, int userId, int year, string number)
        {
            var result = await GetAsync<DataResponse<DocumentNumberBusyDto>>($"/api/v1/Sales/Upd/IsNumberBusy/", new
            {
                firmId,
                userId,
                year,
                number
            });

            return result.Data.IsNumberBusy;
        }

        public Task<SalesUpdRestDto> GetByBaseIdAsync(int firmId, int userId,long documentBaseId)
        {
            return GetAsync<SalesUpdRestDto>($"/api/v1/Sales/Upd/{documentBaseId}", new
            {
                firmId,
                userId
            });
        }

        public Task<List<SalesUpdDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds, CancellationToken cancellationToken)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<SalesUpdDto>());
            }

            var url = $"/SalesUpd/GetByDocumentBaseIds?firmId={firmId}&userId={userId}";

            return PostAsync<IReadOnlyCollection<long>, List<SalesUpdDto>>(url, baseIds, cancellationToken: cancellationToken);
        }

        public Task<List<SalesUpdWithItemsDto>> GetWithItemsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<SalesUpdWithItemsDto>());
            }
            
            return PostAsync<IReadOnlyCollection<long>, List<SalesUpdWithItemsDto>>($"/SalesUpd/GetWithItems?firmId={firmId}&userId={userId}", baseIds);
        }
    }
}