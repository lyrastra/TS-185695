using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.SalesBills.Private;
using Moedelo.Docs.ApiClient.Abstractions.SalesBills.Private.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.SalesBills.Private
{
    [InjectAsSingleton(typeof(IPrivateSalesBillsApiClient))]
    public class PrivateSalesBillsApiClient(
        IHttpRequestExecuter httpRequestExecutor,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<SalesBillsApiClient> logger)
        : BaseApiClient(httpRequestExecutor,
            uriCreator,
            auditTracer,
            authHeadersGetter,
            auditHeadersGetter,
            settingRepository.Get("BillsApiEndpoint"),
            logger), IPrivateSalesBillsApiClient
    {
        public async Task<IReadOnlyList<FetchBillModelDto>> FetchAsync(FetchBillsRequestDto request, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            var response =
                await PostAsync<FetchBillsRequestDto, DataResponse<IReadOnlyList<FetchBillModelDto>>>(
                    "/private/api/v1/Sales/Fetch", request, cancellationToken: ct);
            return response.Data;
        }
    }
}