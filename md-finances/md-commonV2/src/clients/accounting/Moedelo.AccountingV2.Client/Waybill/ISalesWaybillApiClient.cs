using Moedelo.AccountingV2.Dto.Waybills.Sales;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.Waybill
{
    /// <summary>
    /// Клиент для использования external-api "Продажи - Накладные" в private-режиме
    /// </summary>
    public interface ISalesWaybillApiClient : IDI
    {
        /// <summary>
        /// Постраничный список накладных (с фильтрами) 
        /// </summary>
        Task<SalesWaybillCollectionDto> GetAsync(
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
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает накладную по DocumentBaseId (в модели это поле называется Id) 
        /// </summary>
        Task<SalesWaybillDto> GetByBaseIdAsync(int firmId, int userId, long baseId);

        /// <summary>
        /// Сохраняет накладную 
        /// </summary>
        Task<SalesWaybillDto> SaveAsync(int firmId, int userId, SalesWaybillSaveRequestDto dto, HttpQuerySetting setting = null);

        /// <summary>
        /// Возвращает позиции накладных по списку baseId
        /// </summary>
        Task<List<SalesWaybillItemsDto>> GetItemsByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);
    }
}