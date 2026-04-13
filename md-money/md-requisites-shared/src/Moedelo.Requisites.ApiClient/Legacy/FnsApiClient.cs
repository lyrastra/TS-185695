using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Requisites.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IFnsApiClient))]
    internal sealed class FnsApiClient : BaseLegacyApiClient, IFnsApiClient
    {
        public FnsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<FnsApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("FirmRequisitesApiEndpoint"),
                logger)
        {
        }

        public Task<FnsDepartmentDto> GetDepartmentAsync(FirmId firmId, UserId userId)
        {
            var uri = $"/Fns/Department?firmId={firmId}&userId={userId}";
            return GetAsync<FnsDepartmentDto>(uri);
        }

        public Task<FnsDepartmentDto> GetUnifiedBudgetaryPaymentDepartmentAsync(FirmId firmId, UserId userId)
        {
            var uri = $"/Fns/UnifiedBudgetaryPayment?firmId={firmId}&userId={userId}";
            return GetAsync<FnsDepartmentDto>(uri);
        }

        public Task SaveDepartmentAsync(FirmId firmId, UserId userId, FnsDepartmentDto department)
        {
            var uri = $"/Fns/Department?firmId={firmId}&userId={userId}";
            return PostAsync(uri, department);
        }
    }
}