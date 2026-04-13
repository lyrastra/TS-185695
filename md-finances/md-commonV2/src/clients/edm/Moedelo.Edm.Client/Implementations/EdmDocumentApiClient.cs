using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Edm.Client.Contracts;
using Moedelo.Edm.Dto;
using Moedelo.Edm.Dto.Documents;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Edm.Client.Implementations
{
    [InjectAsSingleton]
    public class EdmDocumentApiClient : BaseApiClient, IEdmDocumentApiClient
    {
        private readonly SettingValue apiEndpoint;

        public EdmDocumentApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) :
            base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("EdmPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/Rest/DocumentApi";
        }

        public Task<IEnumerable<EdmLinkedDocumentDto>> GetEdmLinksAsync(int firmId, IEnumerable<long> documentBaseIdList)
        {
            return PostAsync<IEnumerable<long>, IEnumerable<EdmLinkedDocumentDto>>($"/GetEdmLinks?firmId={firmId}", documentBaseIdList);
        }

        public Task CopyDocuments(int fromFirmId, int toFirmId)
        {
            return PostAsync($"/CopyDocuments?fromFirmId={fromFirmId}&toFirmId={toFirmId}");
        }

        public Task SyncWorkflows(List<string> selectedWorkflowIds)
        {
            return PostAsync($"/SyncWorkflows", selectedWorkflowIds);
        }

        public Task<bool> ChangeDocumentsEdmStatusAsync(EdmChangeDocumentsStatusRequest request)
        {
            return PostAsync<EdmChangeDocumentsStatusRequest, bool>("/ChangeDocumentsEdmStatus", request);
        }

        public Task<List<BaseDto>> ParseAndSaveDocumentsAsync(List<SignDocumentsRequest> requests)
        {
            return PostAsync<List<SignDocumentsRequest>, List<BaseDto>>("/ParseAndSaveDocuments", requests);
        }

        public async Task<BaseDto> ParseAndSaveDocumentsAsync(int firmId, int userId, int workflowId)
        {
            var postAsync = await ParseAndSaveDocumentsAsync(new List<SignDocumentsRequest>
                {new SignDocumentsRequest {FirmId = firmId, WorkflowId = workflowId, UserId = userId}});
            return postAsync.First();
        }

        public Task<EdsWizardSendingInfoResponse> GetEdsWizardSendingInfoAsync(int firmId, int docflowId)
            => GetAsync<EdsWizardSendingInfoResponse>("/GetEdsWizardSendingInfo", new { firmId, docflowId });
    }
}
