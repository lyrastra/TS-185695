using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using System;
using Moedelo.BPM.CaseDocs.Client.Dtos;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BPM.CaseDocs.Client
{
    [InjectAsSingleton]
    public class BpmCaseDocsApiClient : BaseApiClient, IBpmCaseDocsApiClient
    {
        private readonly SettingValue apiEndpoint;

        public BpmCaseDocsApiClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("BPMApiUrl");
        }

        #region Case

        public Task<Guid> CaseCreateAsync(CreateCaseForEmailDto data)
        {
            return PostAsync<CreateCaseForEmailDto, Guid>($"/rest/Cases", data);
        }

        public Task<Guid> CaseCreateAsync(Guid accountId, CreateCaseDto data)
        {
            return PostAsync<CreateCaseDto, Guid>($"/rest/Cases?accountId={accountId}", data);
        }

        public async Task<CreateCaseResultDto> CaseCreateAsync(CreateCaseByContactDto data)
        {
            return await PostAsync<CreateCaseByContactDto, CreateCaseResultDto>("/rest/Case/Create2", data).ConfigureAwait(false);
        }

        public async Task<CreateCaseResultDto> CreateWithUserIdFirmIdAsync(CreateCaseByUserAndFirmDto data)
        {
            return await PostAsync<CreateCaseByUserAndFirmDto, CreateCaseResultDto>("/rest/Case/CreateWithUserIdFirmId", data).ConfigureAwait(false);
        }

        public async Task<Dictionary<string, int?>> CheckByMessageIdAsync(List<string> data)
        {
            //limit 20
            var tasks = data
                .Distinct()
                .Select((item, index) => new { item, index })
                .GroupBy(x => x.index / 20)
                .Select(g => g.Select(x => x.item).ToList())
                .Select(d => PostAsync<List<string>, Dictionary<string, int?>>("/rest/Case/CheckByMessageId", d));
            var dics = await Task.WhenAll(tasks).ConfigureAwait(false);
            var result = dics
                .SelectMany(d => d)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
            return result;
        }

        public async Task CaseAddContactsIdsAsync(string caseId, List<ContactDto> data)
        {
            await PostAsync<List<ContactDto>, object>($"/rest/Case/{caseId}/AddContacts", data).ConfigureAwait(false);
        }

        public async Task CaseAddContactIdsAsync(string caseId, List<string> data)
        {
            await PostAsync<List<string>, object>($"/rest/Case/{caseId}/AddContactIds", data).ConfigureAwait(false);
        }

        public async Task CaseAddAccountIdsAsync(string caseId, List<string> data)
        {
            await PostAsync<List<string>, object>($"/rest/Case/{caseId}/AddAccountIds", data).ConfigureAwait(false);
        }
        
        public async Task CaseCancelAsync(string caseId)
        {
            await PostAsync<Task>($"/rest/Case/{caseId}/Cancel").ConfigureAwait(false);
        }

        public async Task SetCaseTypeAsync(string caseId, RequestModelCaseType type)
        {
            await PutAsync($"/rest/Case/{caseId}/SetCaseType", type).ConfigureAwait(false);
        }

        public async Task<CaseInfoDto[]> CaseGetOpenedAsync(int firmId, DateTime? sinceDate = null, string sinceId = null, int limit = 100, params CaseTypeDto[] types)
        {
            return await PostAsync<CaseTypeDto[], CaseInfoDto[]>($"/rest/Case/Opened?firmId={firmId}&sinceDate={sinceDate}&sinceId={sinceId}&limit={limit}", types.ToArray()).ConfigureAwait(false);
        }

        public async Task<CaseInfoDto[]> CaseGetClosedAsync(int firmId, DateTime? sinceDate = null, string sinceId = null, int limit = 100, params CaseTypeDto[] types)
        {
            return await PostAsync<CaseTypeDto[], CaseInfoDto[]>($"/rest/Case/Closed?firmId={firmId}&sinceDate={sinceDate}&sinceId={sinceId}&limit={limit}", types.ToArray()).ConfigureAwait(false);
        }

        public async Task<CaseInfoDto> CaseGetByIdAsync(int firmId, string caseId)
        {
            return await GetAsync<CaseInfoDto>($"/rest/Case/{caseId}?firmId={firmId}").ConfigureAwait(false);
        }
        
        public async Task CaseStateChangeEvent(string caseId, int firmId)
        {
            await PostAsync($"/rest/Case/{caseId}/event/changeState?firmId={firmId}").ConfigureAwait(false);
        }
        
        public async Task CaseThreadUpdatedEvent(string caseId, string messageId, int firmId)
        {
            await PostAsync($"/rest/Case/{caseId}/thread/{messageId}/event/update?firmId={firmId}").ConfigureAwait(false);
        }

        #endregion

        #region CaseUpdate

        public Task<Guid> CaseUpdateCreateAsync(Guid caseId, CreateCaseUpdateForEmailDto data)
        {
            return PostAsync<CreateCaseUpdateForEmailDto, Guid>($"/rest/Cases/{caseId}/Updates", data);
        }

        public Task CaseUpdateDataParsedAsync(Guid id, Guid caseId, bool dataParsed)
        {
            return PutAsync($"/rest/Cases/{caseId}/Updates/{id}/dataParsed", new DataParsedDto(dataParsed));
        }

        public async Task<CaseUpdateInfoDto[]> CaseUpdateGetByCaseIdAsync(int firmId, string caseId, DateTime? sinceDate = null, string sinceId = null, int limit = 20)
        {
            return await GetAsync<CaseUpdateInfoDto[]>($"/rest/Case/{caseId}/Update", new {firmId, sinceDate, sinceId, limit}).ConfigureAwait(false);
        }

        public async Task<CaseUpdateInfoDto> CaseUpdateGetByIdAsync(int firmId, string caseId, string caseUpdateId)
        {
            return await GetAsync<CaseUpdateInfoDto>($"/rest/Case/{caseId}/Update/{caseUpdateId}?firmId={firmId}").ConfigureAwait(false);
        }

        #endregion

        #region Document

        public async Task<string> DocumentCreateForCaseAsync(Guid caseId, HttpFileModel file)
        {
            return await SendFileAsync<string>($"/rest/Cases/{caseId}/Documents", file)
                .ConfigureAwait(false);
        }

        public async Task<string> DocumentCreateForCaseUpdateAsync(Guid caseId, Guid caseUpdateId,
            HttpFileModel file)
        {
            return await SendFileAsync<string>($"/rest/Cases/{caseId}/Updates/{caseUpdateId}/Documents", file)
                .ConfigureAwait(false);
        }

        public async Task<string> DocumentCreateForCaseUpdateAsync(Guid caseId, Guid caseUpdateId,
           string url)
        {
            return await PostAsync<string>($"/rest/Cases/{caseId}/Updates/{caseUpdateId}/Documents?url={url}").ConfigureAwait(false);
        }

        public async Task<string> DocumentCreateForCaseUpdateAsync(Guid caseId, Guid caseUpdateId,
            int firmId, HttpFileModel file)
        {
            return await SendFileAsync<string>($"/rest/Cases/{caseId}/Updates/{caseUpdateId}/Documents?firmId={firmId}", file)
                .ConfigureAwait(false);
        }

        public async Task<string> DocumentCreateForCaseUpdateAsync(Guid caseId, Guid caseUpdateId,
            Guid emailId, HttpFileModel file, bool isLost = false)
        {
            return await SendFileAsync<string>($"/rest/Cases/{caseId}/Updates/{caseUpdateId}/Documents?emailId={emailId}&isLost={isLost}", file)
                .ConfigureAwait(false);
        }

        public async Task<string> DocumentCreateForCaseUpdateAsync(Guid caseId, Guid caseUpdateId,
            Guid emailId, string url)
        {
            return await PostAsync<string>($"/rest/Cases/{caseId}/Updates/{caseUpdateId}/Documents?emailId={emailId}&url={url}").ConfigureAwait(false);
        }

        public async Task<string> DocumentCreateForAccountAsync(int firmId, HttpFileModel file)
        {
            return await SendFileAsync<string>($"/rest/Firms/{firmId}/Documents", file)
                .ConfigureAwait(false);
        }

        public async Task<string> DocumentCreateForAccountAsync(int firmId, string url)
        {
            return await PostAsync<string>($"/rest/Firms/{firmId}/Documents?url={url}").ConfigureAwait(false);
        }

        public Task<string> DocumentCreateForAccountAsync(Guid accountId, HttpFileModel file)
        {
            return SendFileAsync<string>($"/rest/Accounts/{accountId}/Documents", file);
        }
        
        public async Task DocumentUpdateLinksAsync(string documentId)
        {
            await PostAsync<object>($"/rest/Document/{documentId}/UpdateLinks").ConfigureAwait(false);
        }

        public async Task<DocumentDto[]> DocumentGetAllByFirmIdAsync(int firmId, int limit, int offset)
        {
            return await GetAsync<DocumentDto[]>($"/rest/Document/all/{firmId}?offset={offset}&limit={limit}").ConfigureAwait(false);
        }

        public async Task<QueueDocumentDto[]> DocumentGetQueueByFirmIdAsync(int firmId, DateTime? showDocumentDateFrom = null)
        {
            return await GetAsync<QueueDocumentDto[]>($"/rest/Document/queue/{firmId}", new {showDocumentDateFrom}).ConfigureAwait(false);
        }

        public async Task<ArchiveDocumentDto[]> DocumentGetArchiveByFirmIdAsync(int firmId, DateTime? beginDate, DateTime? endDate)
        {
            return await GetAsync<ArchiveDocumentDto[]>($"/rest/Document/archive/{firmId}?beginDate={beginDate}&endDate={endDate}").ConfigureAwait(false);
        }

        public async Task<CaseUpdateAttachedDocumentDto[]> DocumentGetByCaseUpdateIds(string[] caseUpdateIds)
        {
            return await PostAsync<string[], CaseUpdateAttachedDocumentDto[]>("/rest/Document/CaseUpdates", caseUpdateIds).ConfigureAwait(false);
        }

        public Task<DocumentShortInfoDto> DocumentGetShortAsync(string documentId)
        {
            return GetAsync<DocumentShortInfoDto>($"/rest/Document/{documentId}/short");
        }
        
        public Task<int?> DocumentGetFileIdAsync(string documentId)
        {
            return GetAsync<int?>($"/rest/Document/{documentId}/fileId");
        }

        #endregion

        #region Revision

        public Task<DocumentScanDto> DocumentRevisionGetShortAsync(string documentId, string revisionId)
        {
            return GetAsync<DocumentScanDto>($"/Documents/{documentId}/Revisions/{revisionId}/short");
        }

        #endregion

        #region File

        public async Task FileConvertAsync(string documentId)
        {
            await PostAsync<object>($"/rest/DocumentFile/{documentId}/Convert").ConfigureAwait(false);
        }

        public async Task FileUploadAsync(string documentId, HttpFileModel file)
        {
            await SendFileAsync<object>($"/rest/DocumentFile/{documentId}/Upload", file).ConfigureAwait(false);
        }

        public async Task FileLinkAsync(string documentId, int fileStorageId)
        {
            await PostAsync<object>($"/rest/DocumentFile/{documentId}/Link/{fileStorageId}").ConfigureAwait(false);
        }

        #endregion
        
        #region Note

        public async Task<NoteDto[]> NotesByCaseUpdateIdsAsync(List<Guid> CaseUpdateIds)
        {
            return await PostAsync<List<Guid>, NoteDto[]>("/rest/Note", CaseUpdateIds).ConfigureAwait(false);
        }

        public async Task<HttpFileModel> GetNoteFile(string noteId)
        {
            return await GetFileAsync($"/rest/Note/File/{noteId}").ConfigureAwait(false);
        }

        #endregion

        #region Task

        public Task TaskCreateAsync(Guid accountId, string subject, string description, TaskTypeDto taskType)
        {
            return PostAsync($"/rest/Accounts/Tasks?accountId={accountId}&subject={subject}&description={description}&taskType={taskType}");
        }

        #endregion

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/casedocs";
        }
    }
}
