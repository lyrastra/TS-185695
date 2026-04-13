using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Statements.Dto;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Statements
{
    /// <summary>
    /// source: https://github.com/moedelo/md-commonV2/blob/master/src/clients/accounting/Moedelo.AccountingV2.Client/Statement/IOutgoingStatementClient.cs
    /// </summary>
    public interface IOutgoingStatementClient
    {
        Task<List<SalesStatementDocDto>> GetByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);

        Task<SalesStatementWithItemsDto[]> GetByBaseIdsWithItemsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Возвращает суммы, покрытые платежными документами
        /// </summary>
        Task<List<PaidSumDto>> GetPaidSumByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Возвращает акт по DocumentBaseId (в модели это поле называется Id)
        /// </summary>
        Task<SalesStatementDto> GetByBaseIdAsync(FirmId firmId, UserId userId, long baseId);

        /// <summary>
        /// Создаёт акт на продажу
        /// </summary>
        Task<SalesStatementDto> SaveAsync(FirmId firmId, UserId userId, SalesStatementSaveRequestDto saveDto);
    }
}