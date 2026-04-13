using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.Common.Dto;
using Moedelo.Money.ApiClient.Abstractions.Money;
using Moedelo.Money.ApiClient.Abstractions.Money.Dto;

namespace Moedelo.Money.ApiClient.Money
{
    [InjectAsSingleton(typeof(IOperationsAccessApiClient))]
    internal sealed class OperationsAccessApiClient : BaseApiClient, IOperationsAccessApiClient
    {
        public OperationsAccessApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<OperationsAccessApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("MoneyApiEndpoint"),
                logger)
        {
        }

        public async Task<OperationsAccessDto> GetAccessAsync()
        {
            var response = await GetAsync<ApiDataDto<OperationsAccessDto>>("/api/v1/operations/access");
            return response.data;
        }
    }
}
