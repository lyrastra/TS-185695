using System;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Statements.Sales;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.Statement
{
    /// <summary>
    /// Клиент для использования external-api "Продажи - Акты" в private-режиме
    /// </summary>
    public interface ISalesStatementApiClient : IDI
    {
        /// <summary>
        /// Постраничный список актов (с фильтрами) 
        /// </summary>
        Task<SalesStatementCollectionDto> GetAsync(
            int firmId,
            int userId,
            uint pageNo = 1,
            uint pageSize = 50,
            string number = null,
            DateTime? docAfterDate = null,
            DateTime? docBeforeDate = null,
            DateTime? afterDate = null,
            DateTime? beforeDate = null,
            int? kontragentId = null);
        
        /// <summary>
        /// Возвращает акт по DocumentBaseId (в модели это поле называется Id) 
        /// </summary>
        Task<SalesStatementDto> GetByBaseIdAsync(int firmId, int userId, long baseId);
        
        /// <summary>
        /// Сохраняет акт 
        /// </summary>
        Task<SalesStatementDto> SaveAsync(int firmId, int userId, SalesStatementSaveRequestDto dto);
    }
}