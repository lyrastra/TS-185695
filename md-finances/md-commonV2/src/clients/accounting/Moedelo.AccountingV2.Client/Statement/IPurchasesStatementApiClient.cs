using System;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Statements.Purchases;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.Statement
{
    /// <summary>
    /// Клиент для использования external-api "Покупки - Акты" в private-режиме
    /// </summary>
    public interface IPurchasesStatementApiClient : IDI
    {
        /// <summary>
        /// Постраничный список актов (с фильтрами) 
        /// </summary>
        Task<PurchasesStatementCollectionDto> GetAsync(
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
        Task<PurchasesStatementDto> GetByBaseIdAsync(int firmId, int userId, long baseId);
        
        /// <summary>
        /// Сохраняет акт 
        /// </summary>
        Task<PurchasesStatementDto> SaveAsync(int firmId, int userId, PurchasesStatementSaveRequestDto dto);
    }
}