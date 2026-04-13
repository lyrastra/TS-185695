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
    [InjectAsSingleton(typeof(IPfrApiClient))]
    internal sealed class PfrApiClient : BaseLegacyApiClient, IPfrApiClient
    {
        public PfrApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PfrApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("FirmRequisitesApiEndpoint"),
                logger)
        {
        }

        public Task<PfrDepartmentDto> GetDepartmentAsync(FirmId firmId, UserId userId)
        {
            var uri = $"/Pfr/Department?firmId={firmId}&userId={userId}";
            return GetAsync<PfrDepartmentDto>(uri);
        }

        public Task SaveDepartmentAsync(FirmId firmId, UserId userId, PfrDepartmentDto department)
        {
            var uri = $"/Pfr/Department?firmId={firmId}&userId={userId}";
            return PostAsync(uri, department);
        }
    }
}