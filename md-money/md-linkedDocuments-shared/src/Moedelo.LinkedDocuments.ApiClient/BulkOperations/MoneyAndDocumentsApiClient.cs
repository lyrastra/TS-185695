using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BulkOperations;

namespace Moedelo.LinkedDocuments.ApiClient.BulkOperations
{
    [InjectAsSingleton(typeof(IMoneyAndDocumentsApiClient))]
    internal sealed class MoneyAndDocumentsApiClient : BaseApiClient, IMoneyAndDocumentsApiClient
    {
        public MoneyAndDocumentsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<MoneyAndDocumentsApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("LinkedDocumentsApiEndpoint"),
                logger)
        {
        }
        
        public async Task<bool> IsEnabledAsync()
        {
            var uri = "/api/v1/BulkOperations/MoneyAndDocuments/AutoLinking/IsEnabled";
            var response = await GetAsync<DataResponse<bool>>(uri);
            return response.Data;
        }

        public Task RelinkAsync(DateTime? endDate = null, HttpQuerySetting setting = null)
        {
            var uri = $"/api/v1/BulkOperations/MoneyAndDocuments/AutoLinking/Relink?endDate={endDate:yyyy-MM-dd}";
            return PostAsync(uri, setting: setting);
        }
    }
}