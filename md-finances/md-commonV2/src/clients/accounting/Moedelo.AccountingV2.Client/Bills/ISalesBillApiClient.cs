using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Bills;
using Moedelo.AccountingV2.Dto.Bills.Simple.SalesBill;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.Bills
{
    public interface ISalesBillApiClient : IDI
    {
        /// <summary>
        /// Получает список счетов по заданным условиям (неполные модели)
        /// </summary>
        Task<SalesBillCollectionDto> GetAsync(int firmId, int userId, SalesBillPaginationRequestDto criteria);
        
        /// <summary>
        /// Возвращает список облегченных моделей счетов с позициями.
        /// Метод используется в md-stock для журнала документов на странице Товароучёт - Движения.
        /// </summary>
        Task<List<SalesBillSimpleDto>> GetWithItemsAsync(int firmId, int userId, SalesBillPaginationRequestDto criteria,
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Получает позиции для списка счетов
        /// </summary>
        Task<List<SalesBillItemsDto>> GetItemsByBillBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);
        
        /// <summary>
        ///  Используется в md-payment-import (не использовать для других нужд)
        /// </summary>
        Task<SalesBillFullCollectionDto> GetForInternalAsync(int firmId, int userId, SalesBillPaginationRequestDto criteria);
        
        Task<SalesBillDto> SaveAsync(int firmId, int userId, SalesBillSaveRequestDto dto);
        
        Task<SalesBillDto> GetByBaseIdAsync(int firmId, int userId, long baseId);

        Task<List<SalesBillDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Перерасчитать статус покрытия счетов первичными док-тами по списку DocumentBaseId
        /// </summary>
        /// <param name="firmId"></param>
        /// <param name="userId"></param>
        /// <param name="baseIds"></param>
        /// <returns></returns>
        Task UpdateStatusAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Перерасчитать статус оплаты счетов по списку DocumentBaseId
        /// </summary>
        Task UpdatePaymentStatusAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);
    }
}