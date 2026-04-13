using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Statements.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Statements
{
    /// <summary>
    /// source: https://github.com/moedelo/md-commonV2/blob/master/src/clients/accounting/Moedelo.AccountingV2.Client/Statement/IIncomingStatementClient.cs
    /// </summary>
    public interface IIncomingStatementClient
    {
        Task<List<PurchasesStatementDocDto>> GetByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Возвращает акты (покупки) и их позиции по списку DocumentBaseId 
        /// </summary>
        Task<PurchasesStatementWithItemsDto[]> GetByBaseIdsWithItemsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Возвращает суммы, покрытые платежными документами
        /// </summary>
        Task<List<PaidSumDto>> GetPaidSumByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Создает входящий акт
        /// </summary>
        Task<PurchasesStatementResponseDto> SaveAsync(FirmId firmId, UserId userId, PurchasesStatementSaveRequestDto saveDto);
    }
}