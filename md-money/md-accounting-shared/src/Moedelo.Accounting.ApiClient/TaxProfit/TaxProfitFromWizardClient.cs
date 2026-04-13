using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.TaxProfit;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Accounting.ApiClient.TaxProfit
{
    [InjectAsSingleton(typeof(ITaxProfitFromWizardClient))]
    public class TaxProfitFromWizardClient : BaseApiClient, ITaxProfitFromWizardClient
    {
        public TaxProfitFromWizardClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<ITaxProfitFromWizardClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
        }

        public async Task<TaxPostingsValue> GetTaxPostingsProfitAsync(FirmId firmId, UserId userId, DateTime startDate, DateTime endDate, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var uri = $"/TaxProfitWidget";
            var result = await GetAsync<TaxPostingsValue>(uri, new {firmId, userId, startDate=$"{startDate:yyyy-MM-dd}", endDate=$"{endDate:yyyy-MM-dd}"},
                setting: new HttpQuerySetting(), cancellationToken: ct);

            return result;
        }
    }
}