using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.ReceiptStatement.ApiClient.Abstractions;
using Moedelo.ReceiptStatement.ApiClient.Abstractions.Dto;
using System.Threading.Tasks;

namespace Moedelo.ReceiptStatement.ApiClient
{
    [InjectAsSingleton(typeof(IReceiptStatementApiClient))]
    public class ReceiptStatementApiClient : BaseApiClient, IReceiptStatementApiClient
    {
        public ReceiptStatementApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<ReceiptStatementApiClient> logger)
            : base(httpRequestExecuter,
                  uriCreator,
                  auditTracer,
                  authHeadersGetter,
                  auditHeadersGetter,
                  settingRepository.Get("ReceiptStatementApiEndpoint"),
                  logger)
        {
        }

        public async Task<ReceiptStatementDto> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await GetAsync<DataResponse<ReceiptStatementDto>>($"/api/v1/{documentBaseId}").ConfigureAwait(false);
            return response.Data;
        }
    }
}
