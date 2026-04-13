using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.AutoBilling;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Billing.Clients.AutoBilling;

[InjectAsSingleton(typeof(IAutoBillingExtraDataApiClient))]
public class AutoBillingExtraDataApiClient(
    IHttpRequestExecuter httpRequestExecutor,
    IUriCreator uriCreator,
    IAuditTracer auditTracer,
    IAuthHeadersGetter authHeaderGetter,
    IAuditHeadersGetter auditHeadersGetter,
    ISettingRepository settingRepository,
    ILogger<AutoBillingExtraDataApiClient> logger) : BaseApiClient(
        httpRequestExecutor,
        uriCreator,
        auditTracer,
        authHeaderGetter,
        auditHeadersGetter,
        settingRepository.Get("AutoBillingApiEndpoint"),
        logger), IAutoBillingExtraDataApiClient
{
    public Task<int> GetAverageTurnoverAsync(int firmId, DateTime startDate, DateTime endDate)
    {
        const string uri = "v1/data/averageTurnover";
        
        return GetAsync<int>(uri, queryParams: new { firmId, startDate, endDate });
    }

    public Task<int> GetWorkersCountAsync(int firmId, DateTime startDate, DateTime endDate)
    {
        const string uri = "v1/data/workersCount";

        return GetAsync<int>(uri, queryParams: new { firmId, startDate, endDate });
    }

    public Task<int> GetWorkersCountOnDateAsync(
        int firmId, DateTime date)
    {
        const string uri = "v1/data/currentWorkersCount";

        var queryParams = new
        {
            firmId, date = date.ToString("yyyy-MM-dd HH:mm:ss")
        };

        return GetAsync<int>(uri, queryParams: queryParams);
    }

    public Task<int> GetAverageWorkersCountAsync(int firmId, DateTime startDate, DateTime endDate)
    {
        const string uri = "v1/data/averageWorkersCount";
        
        var queryParams = new
        {
            firmId,
            startDate = startDate.ToString("yyyy-MM-dd"),
            endDate = endDate.ToString("yyyy-MM-dd")
        };

        return GetAsync<int>(uri, queryParams: queryParams);
    }
}
