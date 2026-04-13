using Moedelo.Edm.Dto.Kontragent;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Edm.Client.Contracts
{
    public interface IEdmKontragentApiClient : IDI
    {
        Task<bool> IsIntegratedKontragentAsync(int firmId, int userId, int kontragentId);
        Task<IEnumerable<KontragentEdmStatusDto>> GetKontragentsEdmStatusAsync(int firmId, int userId, IEnumerable<int> idList);
        Task<List<FirmEdmKontragentDto>> GetEdmKontragents(int firmId);
        Task<List<KontragentWithInvitationDto>> GetKontragentsWithInvitesAsync(int firmId);
    }
}
