using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Edm.Dto.TsWizard;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Edm.Client.Contracts
{
    public interface ITsWizardClient : IDI
    {
        Task<InviteInfoDto> GetInviteInfoAsync(int firmId, int userId, int inviteId);
        Task<SetEdmLinkResponseDto> SetEdmLinkAsync(int firmId, int userId, int inviteId, string newGuid, int edmSystem);
        Task<UnreadDocflowsInfoDto> GetUnreadDocflowsInfoAsync(int firmId, int userId, int inviteId);
        Task<DocflowsInfoDto> GetDocflowsInfoAsync(int firmId, int userId, int inviteId, int skip, int take);
        Task<SetEdmLinkResponseDto> SwitchEdmLinkAsync(int firmId, int userId, int inviteId, string newGuid,
            int edmSystem);
        Task MoveRelationAsync(int firmId, int userId, int inviteId, int newKontragentId);
        Task<KontragentsListDto> GetKontragentsAsync(int firmId, int userId);
        Task<IReadOnlyList<SimilarKontragentDto>> GetSimilarKontragentsByInviteIdAsync(int firmId, int userId,
            int inviteId);
        Task<OrphanedKontragentInfoDto> GetOrphanedKontragentInfoAsync(int firmId, int userId, string kontragentEdmId);
        Task<SetEdmLinkResponseDto> AddKontragentAsync(int firmId, int userId, int localKontragentId,
            string kontragentEdmId);
        Task<CreateKontragentResponseDto> CreateKontragentAsync(int firmId, int userId, string kontragentEdmId);
        Task<DocflowCreationInfoDto> GetSignedDocflowsAsync(int firmId, int userId, int inviteId, int skip, int take);
    }
}