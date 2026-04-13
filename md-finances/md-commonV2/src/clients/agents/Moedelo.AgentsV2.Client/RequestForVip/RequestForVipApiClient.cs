using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AgentsV2.Dto.Enums;
using Moedelo.AgentsV2.Dto.Partners;
using Moedelo.AgentsV2.Dto.RequestForVip;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AgentsV2.Client.RequestForVip
{
    [InjectAsSingleton]
    public class RequestForVipApiClient : BaseApiClient, IRequestForVipApiClient
    {
        private readonly SettingValue apiEndPoint;

        public RequestForVipApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager
        ) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AgentsApiUrl");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<ResponseStatusCode> RemoveRequestForVip(PartnerIdDto partnerIdDto)
        {
            var result = await PostAsync<PartnerIdDto, StatusWrapper<ResponseStatusCode>>("/RequestForVip/RemoveRequestForVip", partnerIdDto).ConfigureAwait(false);
            return result.StatusCode;
        }

        public async Task<ResponseStatusCode> ApproveRequestForVip(ApproveRequestForVipDto approveRequestForVipDto)
        {
            var result = await PostAsync<ApproveRequestForVipDto, StatusWrapper<ResponseStatusCode>>("/RequestForVip/ApproveRequestForVip", approveRequestForVipDto).ConfigureAwait(false);
            return result.StatusCode;
        }

        public async Task<int> GetNotApprovedVipRequestCount()
        {
            var result = await GetAsync<DataWrapper<int>>("/RequestForVip/GetNotApprovedVipRequestCount").ConfigureAwait(false);
            return result.Data;
        }

        public async Task<List<RequestForVipDto>> GetNotApprovedVipRequests(int page, int pageSize)
        {
            var result = await GetAsync<DataWrapper<List<RequestForVipDto>>>("/RequestForVip/GetNotApprovedVipRequests", new { page, pageSize }).ConfigureAwait(false);
            return result.Data;
        }
    }
}
