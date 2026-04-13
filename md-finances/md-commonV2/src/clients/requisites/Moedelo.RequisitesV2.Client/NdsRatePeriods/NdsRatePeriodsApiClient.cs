using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RequisitesV2.Dto;
using Moedelo.RequisitesV2.Dto.NdsRatePeriods;

namespace Moedelo.RequisitesV2.Client.NdsRatePeriods
{
    [InjectAsSingleton(typeof(INdsRatePeriodsApiClient))]
    public class NdsRatePeriodsApiClient : BaseCoreApiClient, INdsRatePeriodsApiClient
    {
        private readonly SettingValue apiEndPoint;
        private const string ControllerName = "/api/v1/NdsRatePeriods";

        public NdsRatePeriodsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("RequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerName;
        }

        public async Task<NdsRatePeriodDto[]> GetAsync(int firmId, int userId, CancellationToken ct = default)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId, ct)
                .ConfigureAwait(false);

            var response = await GetAsync<ApiDataResult<NdsRatePeriodDto[]>>(
            "", 
            queryHeaders: tokenHeaders,
            cancellationToken: ct).ConfigureAwait(false);

            return response.Data;
        }
    }
}
