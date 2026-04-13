using Moedelo.AccountingV2.Dto.Waybills.Purchases;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.Waybill
{
    /// <summary>
    /// Клиент для использования external-api "Покупки - Накладные" в private-режиме
    /// </summary>
    public interface IPurchasesWaybillApiClient : IDI
    {
        /// <summary>
        /// Постраничный список накладных (с фильтрами) 
        /// </summary>
        Task<PurchasesWaybillCollectionDto> GetAsync(
            int firmId,
            int userId,
            uint pageNo = 1,
            uint pageSize = 50,
            string number = null,
            DateTime? docAfterDate = null,
            DateTime? docBeforeDate = null,
            DateTime? afterDate = null,
            DateTime? beforeDate = null,
            int? kontragentId = null,
            long? stockId = null, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Возвращает накладную по DocumentBaseId (в модели это поле называется Id) 
        /// </summary>
        Task<PurchasesWaybillDto> GetByBaseIdAsync(int firmId, int userId, long baseId);
        
        /// <summary>
        /// Сохраняет накладную 
        /// </summary>
        Task<PurchasesWaybillDto> SaveAsync(int firmId, int userId, PurchasesWaybillSaveRequestDto dto, HttpQuerySetting setting = null);

        /// <summary>
        /// Возвращает позиции накладных по baseIds
        /// </summary>
        Task<List<PurchasesWaybillItemsDto>> GetItemsByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);
    }
}