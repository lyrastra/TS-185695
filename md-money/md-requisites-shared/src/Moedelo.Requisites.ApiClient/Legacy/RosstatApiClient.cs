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
    [InjectAsSingleton(typeof(IRosstatApiClient))]
    internal sealed class RosstatApiClient : BaseLegacyApiClient, IRosstatApiClient
    {
        public RosstatApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<RequisitesAccessClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("FirmRequisitesApiEndpoint"),
                logger)
        {
        }

        public Task<RosstatDepartmentDto> GetDepartmentAsync(FirmId firmId, UserId userId)
        {
            return GetAsync<RosstatDepartmentDto>("/Rosstat/Department", new { firmId, userId });
        }

        public Task SaveDepartmentAsync(FirmId firmId, UserId userId, RosstatDepartmentDto department)
        {
            return PostAsync($"/Rosstat/Department?firmId={firmId}&userId={userId}", department);
        }
    }
}