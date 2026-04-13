using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Statements;
using Moedelo.AccountingV2.Dto.Statements.Purchases;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.Statement
{
    /// <summary>
    /// Клиент для работы с актами из Покупок
    /// </summary>
    public interface IIncomingStatementClient : IDI
    {
        /// <summary>
        /// Возвращает акты (без Items) по списку DocumentBaseId (модель неполная, можно расширять) 
        /// </summary>
        Task<List<PurchasesStatementDocDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);
        
        /// <summary>
        /// Возвращает акты (покупки) и их позиции по списку DocumentBaseId 
        /// </summary>
        Task<List<PurchasesStatementWithItemsDto>> GetByBaseIdsWithItemsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Возвращает список DocumentBaseId актов в покупках(необходимо для консоли BizToAccTransfer)
        /// </summary>
        Task<List<long>> GetStatementBaseIdsAsync(int firmId, int userId, long offset, int count, DateTime? date);

        /// <summary>
        /// Возвращает акт в покупках(c Items) по DocumentBaseId
        /// </summary>
        Task<StatementWithItemDto> GetStatementByBaseIdAsync(int firmId, int userId, long baseId);
    }
}