using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.RequisitesV2.Client.FirmRequisites.Models;
using Moedelo.RequisitesV2.Dto.FirmRequisites;

namespace Moedelo.RequisitesV2.Client.FirmRequisites
{
    public interface IFirmRequisitesClient
    {
        Task<FirmDto> GetFirmByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<List<FirmDto>> GetFirmsByIdsAsync(List<int> ids);

        Task<RegistrationDataDto> GetRegistrationDataAsync(int firmId, int userId, CancellationToken cancellationToken = default);

        Task<List<FirmRegistrationDataDto>> GetFirmsRegistrationDataAsync(IReadOnlyCollection<int> firmIds);

        Task SaveRegistrationDataAsync(int firmId, int userId, RegistrationDataDto data);

        Task<DirectorDto> GetDirectorAsync(int firmId, int userId);
        Task SaveDirectorAsync(int firmId, int userId, DirectorDto director);

        Task SetDirectorAsync(int firmId, int userId, int directorId);

        Task CleanDirectorAsync(int firmId, int userId);

        Task SetInFaceAsync(int firmId, int userId, DirectorRequisitesDto inFace);

        Task<PassportDataDto> GetPassportDataAsync(int firmId, int userId);

        Task SavePassportDataAsync(int firmId, int userId, PassportDataDto data);

        Task SetEmployerModeAsync(int firmId, int userId, bool isEmployerMode);

        /// <summary>
        /// для использования в tester
        /// </summary>
        Task FillByInnAsync(int firmId, int userId, string inn, bool withDirector = true);

        Task<FindByInnResponse> FindByInn(int firmId, int userId, string inn);

        Task<List<int>> GetFirmIdsByInnAsync(string inn);

        /// <summary>
        /// Получить список моделей с базовой информацией о фирме
        /// </summary>
        /// <param name="firmIds">Список идентификаторов фирм</param>
        /// <returns>Список моделей с базовой информацией о фирме</returns>
        Task<List<FirmShortInfoDto>> GetFirmShortInfosAsync(IReadOnlyCollection<int> firmIds);

        /// <summary>
        /// Получить список моделей с базовой информацией о фирме (не блокируется при дебаге на проде)
        /// </summary>
        /// <param name="firmIds">Список идентификаторов фирм</param>
        /// <returns>Список моделей с базовой информацией о фирме</returns>
        Task<List<FirmShortInfoWithoutPhoneDto>> GetFirmShortInfosWithoutPhoneAsync(IReadOnlyCollection<int> firmIds);

        /// <summary>
        /// Выставить ОПФ
        /// </summary>
        /// <param name="firmId">идентификатор фирмы</param>
        /// <param name="userId">идентификатор пользователя фирмы</param>
        /// <param name="isOoo">true - выставить ООО, false - выставить ИП</param>
        /// <param name="cleanInvalidOpfData">true - зачистить данные, которые становятся невалидными при переключении ОПФ</param>
        Task SetOpfAsync(int firmId, int userId, bool isOoo, bool cleanInvalidOpfData = false);

        Task<FirmDto> GetFirmByInnAsync(string inn);

        Task<FirmDto> GetFirmByInnAndLegalUserLoginAsync(string inn, string legalUserLogin);

        Task<PfrAgreementInfoDto> GetPfrAgreementInfo(int firmId);

        Task MoveDocuments(int firmId, int userId, int oldFirmId, int newFirmId);

        Task<RequisitesForFormDto> GetRequisitesForFormByFirmId(int firmId);

        Task<FirmRequisitesDto> GetFirmRequisitesAsync(int firmId);
        
        Task CreateDefaultRequisitesForNewFirmAsync(int firmId);
        Task SaveFirmRequisitesAsync(FirmRequisitesDto firmRequisites);
        Task SaveRequisitesForAccountingReportAsync(
            int firmId,
            int userId,
            RequisitesForAccountingReportDto requisitesForAccountingReport);
        Task SaveOkfsAsync(int firmId, int userId, string okfs);
        Task SetManualCashModeAsync(int firmId, int userId, bool isManualMode, CancellationToken cancellationToken);
    }
}
