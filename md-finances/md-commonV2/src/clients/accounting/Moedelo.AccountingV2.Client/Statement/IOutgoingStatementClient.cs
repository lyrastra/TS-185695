using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Statements;
using Moedelo.AccountingV2.Dto.Statements.Sales;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.Statement
{
    /// <summary>
    /// Клиент для работы с актами из Продаж
    /// </summary>
    public interface IOutgoingStatementClient : IDI
    {
        /// <summary>
        /// Возвращает акты (без Items) по списку DocumentBaseId (модель неполная, можно расширять) 
        /// </summary>
        Task<List<SalesStatementDocDto>> GetByBaseIdsAsync(int firmId, int userId,
            IReadOnlyCollection<long> baseIds);
        
        /// <summary>
        /// Возвращает акты и позиции (Items) по списку DocumentBaseId 
        /// </summary>
        Task<List<SalesStatementWithItemsDto>> GetByBaseIdsWithItemsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Возвращает список DocumentBaseId актов в продажах(необходимо для консоли BizToAccTransfer)
        /// </summary>
        Task<List<long>> GetStatementBaseIdsAsync(int firmId, int userId, long offset, int count, DateTime? date);

        /// <summary>
        /// Возвращает акт в покупках(c Items) по DocumentBaseId
        /// </summary>
        Task<StatementWithItemDto> GetStatementByBaseIdAsync(int firmId, int userId, long baseId);
    }
}