using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Eds.Dto.EdsStatus;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Eds.Client.EdsStatus
{
    [InjectAsSingleton]
    public sealed class EdsStatusClient : BaseCoreApiClient, IEdsStatusClient
    {
        private readonly SettingValue apiEndpoint;

        public EdsStatusClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator,
            IResponseParser responseParser, ISettingRepository settingRepository, IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager) : base(httpRequestExecutor, uriCreator,
            responseParser, settingRepository, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("EdsPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return $"{apiEndpoint.Value}/api/v1";
        }

        public Task<IReadOnlyList<DelayedEdsRegistration>> GetDelayedRegistrationsAsync()
        {
            return GetAsync<IReadOnlyList<DelayedEdsRegistration>>($"/EdsStatus/GetDelayedRegistrations");
        }

        public Task SaveEdsCommentAsync(SaveEdsCommentRequest request)
        {
            return PostAsync($"/EdsStatus/SaveEdsComment", request);
        }

        public Task<RevokedEdsInfo> GetRevokedEdsInfoAsync(GetRevokedEdsInfoRequest request)
        {
            return PostAsync<GetRevokedEdsInfoRequest, RevokedEdsInfo>("/EdsStatus/GetRevokedEdsInfo", request);
        }
    }
}