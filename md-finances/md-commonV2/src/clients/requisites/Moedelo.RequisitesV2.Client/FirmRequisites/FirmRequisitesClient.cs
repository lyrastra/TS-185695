using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RequisitesV2.Client.FirmRequisites.Models;
using Moedelo.RequisitesV2.Dto.FirmRequisites;

namespace Moedelo.RequisitesV2.Client.FirmRequisites
{
    [InjectAsSingleton(typeof(IFirmRequisitesClient))]
    public class FirmRequisitesClient : BaseApiClient, IFirmRequisitesClient
    {
        private readonly SettingValue apiEndPoint;
        
        public FirmRequisitesClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<FirmDto> GetFirmByIdAsync(int id, CancellationToken cancellationToken)
        {
            var url = $"/FirmRequisites/GetFirmById?id={id}";
            
            return GetAsync<FirmDto>(url, cancellationToken: cancellationToken);
        }

        public Task<List<FirmDto>> GetFirmsByIdsAsync(List<int> ids)
        {
            return PostAsync<List<int>, List<FirmDto>>("/FirmRequisites/GetFirmsByIds", ids);
        }

        public Task<RegistrationDataDto> GetRegistrationDataAsync(int firmId, int userId,
            CancellationToken cancellationToken)
        {
            const string uri = "/Firm/RegistrationData";
            var queryParams = new { firmId, userId };

            return GetAsync<RegistrationDataDto>(uri, queryParams, cancellationToken: cancellationToken);
        }

        public Task<List<FirmRegistrationDataDto>> GetFirmsRegistrationDataAsync(IReadOnlyCollection<int> firmIds)
        {
            if (!firmIds.Any())
            {
                return Task.FromResult(new List<FirmRegistrationDataDto>());
            }

            return PostAsync<IReadOnlyCollection<int>, List<FirmRegistrationDataDto>>(
                "/Firm/GetRegistrationDataForFirms",
                firmIds);
        }

        public Task SaveRegistrationDataAsync(int firmId, int userId, RegistrationDataDto data)
        {
            return PostAsync($"/Firm/RegistrationData?firmId={firmId}&userId={userId}", data);
        }

        public Task<DirectorDto> GetDirectorAsync(int firmId, int userId)
        {
            return GetAsync<DirectorDto>("/Firm/Director", new { firmId, userId });
        }

        public Task SaveDirectorAsync(int firmId, int userId, DirectorDto director)
        {
            return PostAsync($"/Firm/Director?firmId={firmId}&userId={userId}", director);
        }

        public Task SetDirectorAsync(int firmId, int userId, int directorId)
        {
            return PostAsync($"/Firm/Director/SetDirector?firmId={firmId}&userId={userId}&directorId={directorId}");
        }

        public Task CleanDirectorAsync(int firmId, int userId)
        {
            return PostAsync($"/Firm/Director/CleanDirector?firmId={firmId}&userId={userId}");
        }

        public Task SetInFaceAsync(int firmId, int userId, DirectorRequisitesDto inFace)
        {
            return PostAsync($"/Firm/Director/SetInFace?firmId={firmId}&userId={userId}", inFace);
        }

        public Task<PassportDataDto> GetPassportDataAsync(int firmId, int userId)
        {
            return GetAsync<PassportDataDto>("/Firm/PassportData", new { firmId, userId });
        }

        public Task SavePassportDataAsync(int firmId, int userId, PassportDataDto data)
        {
            return PostAsync($"/Firm/PassportData?firmId={firmId}&userId={userId}", data);
        }

        public Task SetEmployerModeAsync(int firmId, int userId, bool isEmployerMode)
        {
            return PostAsync($"/FirmRequisites/SetEmployerMode?firmId={firmId}&userId={userId}&isEmployerMode={isEmployerMode}");
        }

        public Task FillByInnAsync(int firmId, int userId, string inn, bool withDirector = true)
        {
            var settings = new HttpQuerySetting { Timeout = TimeSpan.FromSeconds(90) };
            return PostAsync($"/Firm/RegistrationData/FillByInn?firmId={firmId}&userId={userId}&inn={inn}&withDirector={withDirector}", settings);
        }

        public Task<List<int>> GetFirmIdsByInnAsync(string inn)
        {
            return GetAsync<List<int>>($"/Firm/GetFirmIdsByInn?inn={inn}");
        }

        public Task<List<FirmShortInfoDto>> GetFirmShortInfosAsync(IReadOnlyCollection<int> firmIds)
        {
            if (!firmIds.Any())
            {
                return Task.FromResult(new List<FirmShortInfoDto>());
            }

            return PostAsync<IEnumerable<int>, List<FirmShortInfoDto>>("/FirmRequisites/GetFirmShortInfos", firmIds.Distinct());
        }

        public Task<List<FirmShortInfoWithoutPhoneDto>> GetFirmShortInfosWithoutPhoneAsync(IReadOnlyCollection<int> firmIds)
        {
            if (!firmIds.Any())
            {
                return Task.FromResult(new List<FirmShortInfoWithoutPhoneDto>());
            }

            return PostAsync<IEnumerable<int>, List<FirmShortInfoWithoutPhoneDto>>("/FirmRequisites/GetFirmShortInfosWithoutPhone", firmIds.Distinct());
        }

        public Task SetOpfAsync(int firmId, int userId, bool isOoo, bool cleanInvalidOpfData)
        {
            return PostAsync($"/Firm/SetOpf?firmId={firmId}&userId={userId}&isOoo={isOoo}&cleanInvalidOpfData={cleanInvalidOpfData}");
        }

        public Task<FindByInnResponse> FindByInn(int firmId, int userId, string inn)
        {
            return GetAsync<FindByInnResponse>($"/Firm/RegistrationData/FindByInn?firmId={firmId}&userId={userId}&inn={inn}");
        }

        public Task<FirmDto> GetFirmByInnAsync(string inn)
        {
            return GetAsync<FirmDto>("/FirmRequisites/GetFirmByInn", new { inn });
        }

        public Task<FirmDto> GetFirmByInnAndLegalUserLoginAsync(string inn, string legalUserLogin)
        {
            return GetAsync<FirmDto>("/FirmRequisites/GetFirmByInnAndLegalUserLogin", new { inn, legalUserLogin });
        }

        public Task<PfrAgreementInfoDto> GetPfrAgreementInfo(int firmId)
        {
            return GetAsync<PfrAgreementInfoDto>("/FirmRequisites/GetPfrAgreementInfo", new { firmId });
        }

        public Task MoveDocuments(int firmId, int userId, int oldFirmId, int newFirmId)
        {
            return PostAsync($"/EdsUpdate/MoveDocuments?firmId={firmId}&userId={userId}&oldFirmId={oldFirmId}&newFirmId={newFirmId}");
        }

        public Task<RequisitesForFormDto> GetRequisitesForFormByFirmId(int firmId)
        {
            return GetAsync<RequisitesForFormDto>("/FirmRequisites/GetRequisitesForFormByFirmId", new { firmId });
        }

        public Task<FirmRequisitesDto> GetFirmRequisitesAsync(int firmId)
        {
            return GetAsync<FirmRequisitesDto>("/FirmRequisites/GetFirmRequisites", new { firmId });
        }

        public Task CreateDefaultRequisitesForNewFirmAsync(int firmId)
        {
            var uri = $"/FirmRequisites/SaveNewFirmDefaultRequisites?firmId={firmId}";

            return PostAsync(uri);
        }

        public Task SaveFirmRequisitesAsync(FirmRequisitesDto firmRequisites)
        {
            if (firmRequisites.Id == 0)
            {
                throw new Exception("Этот метод предназначен только для редактирования уже существующих записей");
            }

            if (firmRequisites.FirmId == 0)
            {
                throw new Exception("Поле FirmId обязательно должно быть заполнено");
            }

            return PostAsync("/FirmRequisites/SaveFirmRequisites", firmRequisites);
        }

        public Task SaveRequisitesForAccountingReportAsync(
            int firmId,
            int userId,
            RequisitesForAccountingReportDto requisitesForAccountingReport)
        {
            return PostAsync(
                $"/FirmRequisites/SaveRequisitesForAccountingReport?firmId={firmId}&userId={userId}",
                requisitesForAccountingReport);
        }

        public Task SaveOkfsAsync(int firmId, int userId, string okfs)
        {
            return PostAsync($"/FirmRequisites/SaveOkfs?firmId={firmId}&userId={userId}&okfs={okfs}");
        }

        public Task SetManualCashModeAsync(int firmId, int userId, bool isManualMode, CancellationToken cancellationToken)
        {
            var uri = $"/FirmRequisites/ManualCashMode?firmId={firmId}&userId={userId}&value={isManualMode}";

            return PostAsync(uri, cancellationToken: cancellationToken);
        }
    }
}
