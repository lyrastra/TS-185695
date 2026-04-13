using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Accounting.ApiClient.legacy
{
    [InjectAsSingleton(typeof(INdsCodesApiClient))]
    public class NdsCodesApiClient : BaseLegacyApiClient, INdsCodesApiClient
    {
        public NdsCodesApiClient(IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<NdsCodesApiClient> logger
            )
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger
                )
        {
        }

        public async Task<List<NdsCodeDto>> GetByDateAsync(int firmId, int userId, DateTime date)
        {
            var result = await GetAsync<DataResponseWrapper<List<NdsCodeDto>>>("/NdsCodes/GetByDate", new { firmId, userId, date }).ConfigureAwait(false);
            return result.Data;
        }
    }
}