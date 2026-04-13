using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.EdsWizard;
using Moedelo.Common.Enums.Enums.ElectronicReports;
using Moedelo.ErptV2.Client.EdsApi;
using Moedelo.ErptV2.Dto.Eds;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.ErptV2.Client
{
    public interface IEdsWizardApiClient : IDI
    {
        Task UpdatePfrStatus(PfrStatus pfrStatus, int firmId);
        Task UpdateSignatureStatus(SignatureStatus signatureStatus, int firmId, int userId);
        Task<EdsInfo> GetEdsInfo(int firmId, int userId, CancellationToken ctx = default);
        Task<EdsPfrInfo> GetEdsPfrInfoAsync(int firmId, int userId);
        Task<EdsState> GetEdsState(int firmId);
        Task<IList<RequiredDocument>> GetRequiredDocuments(int firmId, bool isOoo, EdsWizardScenario scenario, EdsState edsState, DocumentStepEdsTransferInfo transferInfo = null);
        Task<List<RequiredFileUploadDocuments>> GetUploadedFileDocuments(int firmId);
        Task<FileData> GetRequiredDocumentsArchiveFileAsync(int firmId);
        Task UpdateEdsState(int firmId, int userId, EdsState? edsState = null);
        Task UpdateIsCertificateSigned(int firmId, bool isCertificateSigned);
        Task<EdsResponse> ResetSignatureStatusAsync(int firmId, int userId);
        Task ResetPhoneNumber(int firmId, int userId);
        Task<EdsResponse> CommitEdsDocuments(int firmId, int userId, string userLogin);
        Task ResetStatusAndHistory(int firmId, string userLogin);
        Task<bool> UploadCertificateSigned(int firmId, int userId);
        Task<EdsWizardStateDto> GetCurrentEdsWizardState(int firmId, int userId);
        Task<EdsWizardStateResponse> GetLastEdsWizardState(int firmId);
        Task<CheckRequisitesAndFundsResultDto> IsAnyRequisitesChanged(int firmId, int userId);
        Task<EdsWizardStateResponse> GetLastWizardStartingEvent(int firmId);
        Task<string> GetModifiedEmail(string email);
        Task<bool> IsDocumentsTransferConfirmedAsync(int firmId);
        Task SetIsDocumentsTransferConfirmedAsync(int firmId, bool isDocumentsTransferConfirmed);
        Task<DocumentStepEdsTransferInfo> GetDocumentStepEdsTransferInfoAsync(int firmId);
        Task AddTransferStatementSendedViaEdmEventAsync(int firmId, int dockflowId);
        Task<EdsHistoryDto> GetLastTransferStatementSendedViaEdmEventAsync(int firmId);
        Task<List<EdsHistoryDto>> GetTransferStatementsSendedViaEdmEventAsync(int firmId);
        Task<EdsWizardStateResponse> GetLastWizardStartingEventForPacketGuidAsync(int firmId, string packetId);
        Task<bool> CheckIsTransferViaEdmAsync(int firmId, int startingEventId, string packetId);
        Task<EdsTransferType> GetTransferTypeAsync(int firmId);
        Task<bool> HasConfirmPartnerAsync(int firmId);
        Task ClearTransferSendingEventsAsync(int firmId);
    }

    [InjectAsSingleton]
    public class EdsWizardApiClient : BaseApiClient, IEdsWizardApiClient
    {
        private readonly SettingValue apiEndpoint;

        public EdsWizardApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(
                httpRequestExecutor,
                uriCreator,
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ErptApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/EdsWizardApi";
        }

        public Task UpdatePfrStatus(PfrStatus pfrStatus, int firmId) =>
            GetAsync("/UpdatePfrStatus", new { pfrStatus, firmId });

        public Task UpdateSignatureStatus(SignatureStatus signatureStatus, int firmId, int userId) =>
            GetAsync("/UpdateSignatureStatus", new { signatureStatus, firmId, userId });

        public Task<EdsInfo> GetEdsInfo(int firmId, int userId, CancellationToken ctx) =>
            GetAsync<EdsInfo>("/GetEdsInfo", new { firmId, userId }, cancellationToken: ctx);

        public Task<EdsPfrInfo> GetEdsPfrInfoAsync(int firmId, int userId) =>
            GetAsync<EdsPfrInfo>("/GetEdsPfrInfo", new { firmId, userId });

        public Task<EdsState> GetEdsState(int firmId) =>
            GetAsync<EdsState>("/GetEdsState", new { firmId });

        public Task<IList<RequiredDocument>> GetRequiredDocuments(int firmId, bool isOoo, EdsWizardScenario scenario,
            EdsState edsState, DocumentStepEdsTransferInfo transferInfo = null)
            => PostAsync<object, IList<RequiredDocument>>("/GetRequiredDocuments",
                new {firmId, isOoo, scenario, edsState, transferInfo});

        public Task<FileData> GetRequiredDocumentsArchiveFileAsync(int firmId) =>
            GetAsync<FileData>("/GetRequiredDocumentsArchiveFile", new { firmId });

        public Task<List<RequiredFileUploadDocuments>> GetUploadedFileDocuments(int firmId)
            => GetAsync<List<RequiredFileUploadDocuments>>($"/GetUploadedFileDocuments?firmId={firmId}");
        
        public Task<EdsResponse> ResetSignatureStatusAsync(int firmId, int userId) =>
            PostAsync<EdsResponse>($"/ResetSignatureStatus?firmId={firmId}&userId={userId}");
        
        public Task ResetPhoneNumber(int firmId, int userId) =>
            PostAsync($"/ResetPhoneNumber?firmId={firmId}&userId={userId}");

        public Task UpdateEdsState(int firmId, int userId, EdsState? edsState = null) =>
            PostAsync($"/UpdateEdsState?firmId={firmId}&userId={userId}&edsState={edsState}");

        public Task UpdateIsCertificateSigned(int firmId, bool isCertificateSigned) =>
            PostAsync($"/UpdateIsCertificateSigned?firmId={firmId}&isCertificateSigned={isCertificateSigned}");

        public Task ResetStatusAndHistory(int firmId, string userLogin) =>
            PostAsync($"/ResetStatusAndHistory?firmId={firmId}&userLogin={userLogin}");

        public Task<EdsResponse> CommitEdsDocuments(int firmId, int userId, string userLogin) =>
            PostAsync<EdsResponse>($"/CommitEdsDocuments?firmId={firmId}&userId={userId}&userLogin={userLogin}");

        public Task<bool> UploadCertificateSigned(int firmId, int userId) =>
            PostAsync<bool>($"/UploadCertificateSigned?firmId={firmId}&userId={userId}");

        public Task<EdsWizardStateDto> GetCurrentEdsWizardState(int firmId, int userId) =>
            GetAsync<EdsWizardStateDto>($"/GetCurrentEdsWizardState?firmId={firmId}&userId={userId}");

        public Task<EdsWizardStateResponse> GetLastEdsWizardState(int firmId) =>
            GetAsync<EdsWizardStateResponse>($"/GetLastEdsWizardState?firmId={firmId}");

        public Task<EdsWizardStateResponse> GetLastWizardStartingEvent(int firmId) =>
            GetAsync<EdsWizardStateResponse>($"/GetLastWizardStartingEvent?firmId={firmId}");

        public Task<CheckRequisitesAndFundsResultDto> IsAnyRequisitesChanged(int firmId, int userId) =>
            GetAsync<CheckRequisitesAndFundsResultDto>($"/IsAnyRequisitesChanged?firmId={firmId}&userId={userId}");

        public Task<string> GetModifiedEmail(string email) =>
            GetAsync<string>($"/GetModifiedEmail?email={Uri.EscapeDataString(email)}");

        public Task<bool> IsDocumentsTransferConfirmedAsync(int firmId) =>
            GetAsync<bool>($"/IsDocumentsTransferConfirmed?firmId={firmId}");
        
        public Task SetIsDocumentsTransferConfirmedAsync(int firmId, bool isDocumentsTransferConfirmed) =>
            PostAsync(
                $"/SetIsDocumentsTransferConfirmed?firmId={firmId}&isDocumentsTransferConfirmed={isDocumentsTransferConfirmed}");

        public Task<DocumentStepEdsTransferInfo> GetDocumentStepEdsTransferInfoAsync(int firmId)
            => GetAsync<DocumentStepEdsTransferInfo>($"/GetDocumentStepEdsTransferInfo?firmId={firmId}");

        public Task AddTransferStatementSendedViaEdmEventAsync(int firmId, int dockflowId)
            => PostAsync($"/AddTransferStatementSendedViaEdmEvent?firmId={firmId}&docflowId={dockflowId}");

        public Task<EdsHistoryDto> GetLastTransferStatementSendedViaEdmEventAsync(int firmId)
            => GetAsync<EdsHistoryDto>($"/GetLastTransferStatementSendedViaEdmEvent?firmId={firmId}");

        public Task<List<EdsHistoryDto>> GetTransferStatementsSendedViaEdmEventAsync(int firmId)
            => GetAsync<List<EdsHistoryDto>>($"/GetTransferStatementsSendedViaEdm?firmId={firmId}");

        public Task<EdsWizardStateResponse> GetLastWizardStartingEventForPacketGuidAsync(int firmId, string packetId) =>
            GetAsync<EdsWizardStateResponse>($"/GetLastWizardStartingEventForPacketGuid", new {firmId, packetId});

        public Task<bool> CheckIsTransferViaEdmAsync(int firmId, int startingEventId, string packetId) =>
            GetAsync<bool>($"/CheckIsTransferViaEdm", new {firmId, startingEventId, packetId});

        public Task<EdsTransferType> GetTransferTypeAsync(int firmId) =>
            GetAsync<EdsTransferType>($"/TransferType", new {firmId});

        public Task<bool> HasConfirmPartnerAsync(int firmId) =>
            GetAsync<bool>($"/HasConfirmPartner", new {firmId});

        public Task ClearTransferSendingEventsAsync(int firmId)
            => PostAsync($"/ClearTransferSendingEvents?firmId={firmId}");
    }
}
