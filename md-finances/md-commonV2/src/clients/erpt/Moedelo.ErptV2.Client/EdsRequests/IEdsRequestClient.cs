using System.Threading.Tasks;
using Moedelo.ErptV2.Dto.EdsRequest;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.ErptV2.Client.EdsRequests
{
    public interface IEdsRequestClient : IDI
    {
        Task<EdsRequestChangesDto> GetChangesFromLastSuccessfulRequest(int firmId, int userId);
        Task<EdsRequestDto> GetEdsRequestFromRequisites(int firmId, int userId);
        Task<EdsEgrRequisitesResponse> GetRequisitesFromEgrAsync(int firmId, int userId, string inn, bool isOoo);
        Task<EdsRequestDto> GetRequisitesFromLasSuccessfulRequestAsync(int firmId);
        Task<EdsInfoWithChangesDto> GetEdsInfoWithChangesAsync(int historyEventId);
    }
}