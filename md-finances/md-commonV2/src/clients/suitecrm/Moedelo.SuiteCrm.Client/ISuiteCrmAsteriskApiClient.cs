using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.SuiteCrm.Dto.SendAsterisk;

namespace Moedelo.SuiteCrm.Client
{
    public interface ISuiteCrmAsteriskApiClient : IDI
    {
        Task<OperationResultDto> UpdateStatusAsync(UpdateStatusDto dto);
        
        Task<ObjectsForSendToAsteriskDto> ObjectsForSendToAsteriskAsync();

        Task<ObjectsForLoginOpportunitiesToAsteriskDto> ObjectsForLoginOpportunitiesToAsteriskAsync();

        Task<ObjectsForProcessingLeadsDto> GetObjectsForProcessingLeadsAsync();
        
        Task<ObjectWithValues> GetBucketsAsync(string campaignId);
        
        Task SendToReprocessingAsync(SendToReprocessingDto dto);
        
        Task SendToOverSubscriptionAsync(SendToOverSubscriptionDto dto);
    }
}