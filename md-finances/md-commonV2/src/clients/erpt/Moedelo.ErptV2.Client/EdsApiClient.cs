using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.ElectronicReports;
using Moedelo.ErptV2.Client.EdsApi;
using Moedelo.ErptV2.Dto.Eds;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.ErptV2.Client
{
    public interface IEdsApiClient : IDI
    {
        Task<List<string>> ValidateEdsRequest(int firmId, int userId);
        Task<EdsResponse> SendEdsRequest(int firmId, int userId, string operatorLogin, EdsRequest request);

        Task AddEdsHistoryAsync(EdsHistoryDto addEdsHistoryRequest);

        Task<EdsHistoryEvent?> GetStatusAsync(int firmId);

        Task<ERptStatusResponse> GetERptStatus(int firmId, CancellationToken cancellationToken = default);
        Task<IEnumerable<ERptStatusResponse>> GetERptStatus(IEnumerable<int> firmIds);
        Task<List<FirmIdentificators>> GetFirmIdentificators(List<string> guids);

        Task<EdsHistoryWithRequisitesDto> GetLastRequestAsync(int firmId);

        Task NotifyAboutEdsCreated (int firmId, int userId);
        Task NotifyAboutEdsCreateFailed(int firmId, int userId, int historyId);
        Task NotifyAboutEdsProlongation (int firmId, int userId, DateTime sendDate, string daysBeforeExpiry);
        Task NotifyAboutNeedSignCertificate(int firmId, DateTime sendDate);

        Task<ChangeCodeOfOutgoingDirectionResponse> ChangeCodeOfOutgoingDirection(int firmId, int userId, ChangeCodeOfOutgoingDirectionRequest request);

        Task<AstralRegionInfoDto> GetAstralRegionInfo(string regionCode);
        Task<FileData> GetBankArchiveAsync(int firmId, int userId, string guid);
        Task<EdsRequisitesValidateResponse> ValidateEdsRequisites(EdsRequisitesValidateRequest request);
        Task<bool> IsEdsActiveAsync(int firmId);
        Task<bool> HasEdmTypeInEdsHistory(int firmId);
        Task<EdsProvider> GetCurrentEdsProviderAsync(int firmId);
        Task<EdsProvider> GetActiveEdsProviderAsync(int userContextFirmId);
        Task<EdsProvider> EvaluateEdsProviderAsync(int firmId, int userId);
        Task<bool> IsMoedeloRegionAsync(int firmId);
        Task<EdsExpiry> GetEdsExpiryAsync(int firmId);
        Task<List<EdsHistoryDto>> GetAllEdsHistoryByFirmAsync(int firmId);
        Task<EdsCopyResponse> CopyEdsByOperatorAsync(EdsCopyRequest edsCopyDto);
        Task<bool> IsRequestSignatureRequiredAsync(int firmId);
        Task<bool> IsCryptoCenterAvailableAsync(int firmId, CancellationToken cancellationToken = default);
    }

    [InjectAsSingleton]
    public class EdsApiClient : BaseApiClient, IEdsApiClient
    {
        private readonly SettingValue apiEndpoint;
        
        public EdsApiClient(
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
            return apiEndpoint.Value;
        }

        public Task<ChangeCodeOfOutgoingDirectionResponse> ChangeCodeOfOutgoingDirection(int firmId, int userId, ChangeCodeOfOutgoingDirectionRequest request)
        {
            var firmIdsStr = request.FirmIds.Aggregate(string.Empty, (a,b ) => a + "&FirmIds=" + b);
            return PostAsync<ChangeCodeOfOutgoingDirectionRequest, ChangeCodeOfOutgoingDirectionResponse>(
                $"/eds/ChangeCodeOfOutgoingDirection?firmId={firmId}&userId={userId}&OperationType={request.OperationType}{firmIdsStr}",
                request, 
                null,
                new HttpQuerySetting
                {
                    Timeout = new TimeSpan(0, 10, 0)
                });
        }

        public Task AddEdsHistoryAsync(EdsHistoryDto addEdsHistoryRequest) =>
            PostAsync($"/eds/AddEdsHistoryEventAsync", addEdsHistoryRequest);
        
        public Task<ERptStatusResponse> GetERptStatus(int firmId, CancellationToken cancellationToken) =>
            GetAsync<ERptStatusResponse>($"/eds/GetErStatus/{firmId}", cancellationToken: cancellationToken);

        public Task<IEnumerable<ERptStatusResponse>> GetERptStatus(IEnumerable<int> firmIds) =>
            PostAsync<ERptStatusRequest, IEnumerable<ERptStatusResponse>>($"/eds/eRptStatus", new ERptStatusRequest { FirmIds = firmIds });

        public Task NotifyAboutEdsCreated(int firmId, int userId) =>
            PostAsync($"/ErptNotification/EdsCreated?firmId={firmId}&userId={userId}");

        public Task NotifyAboutEdsCreateFailed(int firmId, int userId, int edsHistoryId) =>
            PostAsync($"/ErptNotification/EdsDocumentsRejected?firmId={firmId}&userId={userId}&edsHistoryId={edsHistoryId}");

        public Task NotifyAboutEdsProlongation(int firmId, int userId, DateTime sendDate, string daysBeforeExpiry) =>
            PostAsync($"/ErptNotification/EdsProlongation?firmId={firmId}&userId={userId}&sendDate={sendDate.ToString("ddMMyyyy")}&daysBeforeExpiry={daysBeforeExpiry}");

        public Task NotifyAboutNeedSignCertificate(int firmId, DateTime sendDate) =>
            PostAsync($"/ErptNotification/EdsSingCertificate?firmId={firmId}&sendDate={sendDate.ToString("ddMMyyyy")}");

        public Task<EdsResponse> SendEdsRequest(int firmId, int userId, string operatorLogin, EdsRequest request) =>
            PostAsync<EdsRequest, EdsResponse>($"/eds/SendEdsRequestV2?firmId={firmId}&userId={userId}&operatorLogin={operatorLogin}", request, setting: new HttpQuerySetting(TimeSpan.FromSeconds(100)));

        public Task<List<string>> ValidateEdsRequest(int firmId, int userId) =>
            GetAsync<List<string>>($"/eds/ValidateEdsRequestV2?firmId={firmId}&userId={userId}");

        public Task<List<FirmIdentificators>> GetFirmIdentificators(List<string> guids) =>
            PostAsync<List<string>, List<FirmIdentificators>>($"/eds/GetFirmIdentificators", guids);

        public Task<EdsHistoryEvent?> GetStatusAsync(int firmId) =>
            GetAsync<EdsHistoryEvent?>($"/eds/GetStatus/{firmId}");

        public Task<EdsHistoryWithRequisitesDto> GetLastRequestAsync(int firmId) =>
            GetAsync<EdsHistoryWithRequisitesDto>($"/eds/GetLastRequestAsync/{firmId}");

        public Task<AstralRegionInfoDto> GetAstralRegionInfo(string regionCode) =>
            GetAsync<AstralRegionInfoDto>("/astral/GetAstralRegionInfo", new { regionCode });
        
        public Task<FileData> GetBankArchiveAsync(int firmId, int userId, string guid) =>
            GetAsync<FileData>("/ReportFile/GetBankArchive", new { firmId, userId, guid });

        public Task<EdsRequisitesValidateResponse> ValidateEdsRequisites(EdsRequisitesValidateRequest request)
        {
            var settings = new HttpQuerySetting(new TimeSpan(0, 1, 40));
            return PostAsync<EdsRequisitesValidateRequest, EdsRequisitesValidateResponse>("/eds/ValidateEdsRequisites", request, setting: settings);
        }

        public Task<bool> IsEdsActiveAsync(int firmId) =>
            GetAsync<bool>($"/eds/IsEdsActive/{firmId}");

        public Task<bool> HasEdmTypeInEdsHistory(int firmId) =>
            GetAsync<bool>($"/eds/HasEdmTypeInEdsHistory/{firmId}");

        public Task<EdsProvider> GetCurrentEdsProviderAsync(int firmId)
            => GetAsync<EdsProvider>($"/eds/GetCurrentEdsProvider/{firmId}");

        public Task<EdsProvider> GetActiveEdsProviderAsync(int firmId)
            => GetAsync<EdsProvider>($"/eds/GetActiveEdsProvider/{firmId}");

        public Task<EdsProvider> EvaluateEdsProviderAsync(int firmId, int userId) =>
            GetAsync<EdsProvider>($"/eds/EvaluateEdsProvider?firmId={firmId}&userId={userId}");

        public Task<bool> IsMoedeloRegionAsync(int firmId) =>
            GetAsync<bool>($"/eds/IsMoedeloRegion/{firmId}");

        public Task<EdsExpiry> GetEdsExpiryAsync(int firmId) =>
            GetAsync<EdsExpiry>($"/eds/GetEdsExpiry?firmId={firmId}");

        public Task<List<EdsHistoryDto>> GetAllEdsHistoryByFirmAsync(int firmId) =>
           GetAsync<List<EdsHistoryDto>>($"/eds/GetAllEdsHistoryByFirm?firmId={firmId}");
           
        public Task<EdsCopyResponse> CopyEdsByOperatorAsync(EdsCopyRequest EdsCopyRequest) =>
            PostAsync<EdsCopyRequest, EdsCopyResponse>($"/eds/CopyEdsByOperator", EdsCopyRequest);

        public Task<bool> IsRequestSignatureRequiredAsync(int firmId) =>
            GetAsync<bool>("/eds/IsRequestSignatureRequired", new { firmId });

        public Task<bool> IsCryptoCenterAvailableAsync(int firmId, CancellationToken cancellationToken)
            => GetAsync<bool>("/eds/IsCryptoCenterAvailable", new { firmId }, cancellationToken: cancellationToken);
    }
}
