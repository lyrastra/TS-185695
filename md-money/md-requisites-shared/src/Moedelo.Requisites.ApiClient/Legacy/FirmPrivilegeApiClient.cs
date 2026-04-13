using System.Collections.Generic;
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

namespace Moedelo.Requisites.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IFirmPrivilegeApiClient))]
    public class FirmPrivilegeApiClient : BaseLegacyApiClient, IFirmPrivilegeApiClient
    {
        public FirmPrivilegeApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<FirmPrivilegeApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("FirmRequisitesApiEndpoint"),
                logger)
        {
        }

        public Task<IReadOnlyList<FirmPrivilegeDto>> GetPrivilegesByYearAsync(int year, int skipCount, int takeCount)
        {
            var uri = $"/Pfr/GetPrivilegesByYear?year={year}&skipCount={skipCount}&takeCount={takeCount}";
            return GetAsync<IReadOnlyList<FirmPrivilegeDto>>(uri);
        }

        public Task<IReadOnlyList<FirmPrivilegeDto>> GetPrivilegesByFirmIdAsync(int firmId)
        {
            var uri = $"/Pfr/GetPrivilegesByFirm?firmId={firmId}";
            return GetAsync<IReadOnlyList<FirmPrivilegeDto>>(uri);
        }
    }
}