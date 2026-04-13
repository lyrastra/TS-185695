using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.AccountingStatement;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.AccountingV2.Client.AccountingStatement
{
    public interface IAccountingStatementApiClient : IDI
    {
        Task CreateAccountingStatementForUsnAsync(int firmId, int userId, ReportStatementDataDto dto);
        Task CreateAccountingStatementForEnvdAsync(int firmId, int userId, IList<ReportStatementDataDto> dto);
        Task<long> CreateAccountingStatementAsync(int firmId, int userId, AccountingStatementDto data);
        Task DeleteAccountingStatement(int firmId, int userId, long statementId);
        Task<List<AccountingStatementSimpleDto>> GetByType(int firmId, int userId, AccountingStatementType type, DateTime startDate, DateTime endDate);
        Task<List<AccountingStatementDto>> GetBySubcontoAsync(int firmId, int userId, long subcontoId, DateTime startDate, DateTime endDate, bool isFromReadOnlyDb = false, HttpQuerySetting setting = null);
        Task<List<AccountingStatementSimpleDto>> GetByBaseIds(int firmId, int userId, IReadOnlyCollection<long> baseIds);
        /// <summary>
        /// Возвращает список DocumentBaseId удалённых документов
        /// </summary>
        Task<List<long>> DeleteBySourceDocumentBaseIdAndTypeAsync(int firmId, int userId, long documentBaseId, AccountingStatementType statementType);

        Task<SavedAccountingStatementDto> CreateAccountingStatementForDissmissFixedAssetAsync(int firmId, int userId, LinkedAccountingStatementDto data);

        Task DeleteByBaseIdAsync(int firmId, int userId, long documentBaseId);

        Task CreateAccountingStatementForContractAsync(int firmId, int userId, int contractId, DateTime? byDate = null);

        /// <summary>
        /// Возвращает список товаров, для которых есть хотя-бы одна ручная бухсправка позднее заданной даты с указанными товарами.
        /// </summary>
        Task<List<long>> GetProductIdsWithManualAccDocumentLaterThanDateAsync(int firmId, int userId, AccDocumentsProductIdsListRequestDto request);

        /// <summary>
        /// Возвращает список товаров, для которых есть любые бухсправки раньше или в указанную дату
        /// </summary>
        Task<List<long>> GetProductIdsWithAnyAccDocumentBeforeDateAsync(int firmId, int userId, AccDocumentsProductIdsListRequestDto request);
    }
}
