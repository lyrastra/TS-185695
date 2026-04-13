using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Edm.Client.Contracts;
using Moedelo.Edm.Dto.TsWizard;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Edm.Client.Implementations
{
    [InjectAsSingleton]
    public class TsWizardClient : BaseApiClient, ITsWizardClient
    {
        private readonly SettingValue apiEndpoint;
        private readonly HttpQuerySetting customSetting = new HttpQuerySetting(TimeSpan.FromMinutes(2));

        public TsWizardClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator,
            IResponseParser responseParser, 
            IAuditTracer auditTracer, 
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : base(
            httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("EdmPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return $"{apiEndpoint.Value}/Rest/TsWizard";
        }

        public Task<InviteInfoDto> GetInviteInfoAsync(int firmId, int userId, int inviteId)
        {
            return GetAsync<InviteInfoDto>("/GetInviteInfo", new { firmId, userId, inviteId }, setting: customSetting);
        }

        public Task<SetEdmLinkResponseDto> SetEdmLinkAsync(int firmId, int userId, int inviteId, string newGuid, int edmSystem)
        {
            return PostAsync<SetEdmLinkResponseDto>($"/SetEdmLink?firmId={firmId}&userId={userId}&inviteId={inviteId}&newGuid={newGuid}&edmSystem={edmSystem}");
        }

        public Task<SetEdmLinkResponseDto> SwitchEdmLinkAsync(int firmId, int userId, int inviteId, string newGuid, int edmSystem)
        {
            return PostAsync<SetEdmLinkResponseDto>($"/SwitchEdmLink?firmId={firmId}&userId={userId}&inviteId={inviteId}&newGuid={newGuid}&edmSystem={edmSystem}");
        }
        
        public Task MoveRelationAsync(int firmId, int userId, int inviteId, int newKontragentId)
        {
            return PostAsync($"/MoveRelation?firmId={firmId}&userId={userId}&inviteId={inviteId}&newKontragentId={newKontragentId}");
        }

        public Task<UnreadDocflowsInfoDto> GetUnreadDocflowsInfoAsync(int firmId, int userId, int inviteId)
        {
            return GetAsync<UnreadDocflowsInfoDto>("/GetUnreadDocflowsInfo", new { firmId, userId, inviteId }, setting: customSetting);
        }

        public Task<DocflowsInfoDto> GetDocflowsInfoAsync(int firmId, int userId, int inviteId, int skip, int take)
        {
            return GetAsync<DocflowsInfoDto>("/GetDocflowsInfo", new { firmId, userId, inviteId, skip, take });
        }

        public Task<DocflowCreationInfoDto> GetSignedDocflowsAsync(int firmId, int userId, int inviteId, int skip, int take)
        {
            return GetAsync<DocflowCreationInfoDto>("/GetSignedDocflows", new { firmId, userId, inviteId, skip, take });
        }

        public Task<KontragentsListDto> GetKontragentsAsync(int firmId, int userId)
        {
            return GetAsync<KontragentsListDto>("/GetKontragents", new { firmId, userId });
        }
        
        public Task<IReadOnlyList<SimilarKontragentDto>> GetSimilarKontragentsByInviteIdAsync(int firmId, int userId,
            int inviteId)
        {
            return GetAsync<IReadOnlyList<SimilarKontragentDto>>("/GetSimilarKontragentsByInviteId", new { firmId, userId, inviteId });
        }

        public Task<OrphanedKontragentInfoDto> GetOrphanedKontragentInfoAsync(int firmId, int userId, string kontragentEdmId)
        {
            return GetAsync<OrphanedKontragentInfoDto>("/GetOrphanedKontragentInfo", new { firmId, userId, kontragentEdmId });
        }

        public Task<SetEdmLinkResponseDto> AddKontragentAsync(int firmId, int userId, int localKontragentId, string kontragentEdmId)
        {
            return PostAsync<SetEdmLinkResponseDto>($"/AddKontragent?firmId={firmId}&userId={userId}&localKontragentId={localKontragentId}&kontragentEdmId={kontragentEdmId}");
        }

        public Task<CreateKontragentResponseDto> CreateKontragentAsync(int firmId, int userId, string kontragentEdmId)
        {
            return PostAsync<CreateKontragentResponseDto>($"/CreateKontragent?firmId={firmId}&userId={userId}&kontragentEdmId={kontragentEdmId}");
        }
    }
}
