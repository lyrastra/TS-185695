using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Requisites.ApiClient.Legacy;

[InjectAsSingleton(typeof(ITaxAdministrationApiClient))]
internal sealed class TaxAdministrationApiClient: BaseLegacyApiClient, ITaxAdministrationApiClient
{
    public TaxAdministrationApiClient(
        IHttpRequestExecuter httpRequestExecuter,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<TaxationSystemApiClient> logger)
        : base(
            httpRequestExecuter,
            uriCreator,
            auditTracer,
            auditHeadersGetter,
            settingRepository.Get("FirmRequisitesApiEndpoint"),
            logger)
    {
    }

    public Task<TaxAdministrationDto> GetByCodeAsync(int firmId, int userId, string code)
    {
        return GetAsync<TaxAdministrationDto>("/TaxAdministration/GetByCode", new { firmId, userId, code });
    }
}