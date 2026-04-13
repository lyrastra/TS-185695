using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.SalesUkd;
using Moedelo.Docs.ApiClient.Abstractions.SalesUkd.Model;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Docs.ApiClient.SalesUkd
{
    [InjectAsSingleton(typeof(ISalesUkdApiClient))]
    public class SalesUkdApiClient : BaseApiClient, ISalesUkdApiClient
    {
        public SalesUkdApiClient(IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SalesUkdApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("UkdApiEndpoint"),
                logger)
        {
        }

        public async Task<Ukd> GetByBaseId(long baseId)
        {
            var ukd = await GetAsync<DataResponse<Ukd>>($"/api/v1/Ukd/{baseId}");
            return ukd.Data;
        }

        public async Task<List<Ukd>> GetByRefundPaymentBaseIdAsync(long baseId)
        {
            var ukd = await GetAsync<DataResponse<List<Ukd>>>($"/api/v1/Ukd/GetByRefundPayment/{baseId}");
            return ukd.Data;
        }
    }
}