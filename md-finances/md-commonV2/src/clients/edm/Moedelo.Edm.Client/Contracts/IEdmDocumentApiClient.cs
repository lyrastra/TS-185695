using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Edm.Dto;
using Moedelo.Edm.Dto.Documents;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Edm.Client.Contracts
{
    public interface IEdmDocumentApiClient : IDI
    {
        Task<IEnumerable<EdmLinkedDocumentDto>> GetEdmLinksAsync(int firmId, IEnumerable<long> documentBaseIdList);

        Task CopyDocuments(int fromFirmId, int toFirmId);

        Task SyncWorkflows(List<string> selectedWorkflowIds);

        Task<bool> ChangeDocumentsEdmStatusAsync(EdmChangeDocumentsStatusRequest request);

        Task<List<BaseDto>> ParseAndSaveDocumentsAsync(List<SignDocumentsRequest> requests);

        Task<BaseDto> ParseAndSaveDocumentsAsync(int firmId, int userId, int workflowId);
        Task<EdsWizardSendingInfoResponse> GetEdsWizardSendingInfoAsync(int firmId, int docflowId);
    }
}
