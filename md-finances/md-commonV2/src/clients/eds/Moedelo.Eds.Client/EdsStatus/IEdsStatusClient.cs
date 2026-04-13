using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Eds.Dto.EdsStatus;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Eds.Client.EdsStatus
{
    public interface IEdsStatusClient : IDI
    {
        Task<IReadOnlyList<DelayedEdsRegistration>> GetDelayedRegistrationsAsync();
        Task SaveEdsCommentAsync(SaveEdsCommentRequest request);
        Task<RevokedEdsInfo> GetRevokedEdsInfoAsync(GetRevokedEdsInfoRequest request);
    }
}