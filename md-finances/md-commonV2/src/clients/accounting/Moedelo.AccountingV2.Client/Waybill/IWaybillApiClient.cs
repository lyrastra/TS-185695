using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.PurchaseInfo;
using Moedelo.AccountingV2.Dto.Waybills;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.AccountingV2.Client.Waybill
{
    // todo: разделить клиент на 2 (покупки и продажи)
    public interface IWaybillApiClient : IDI
    {
        /// <summary>
        /// Пересохраняет документ:
        /// - пересоздает связи
        /// - пересоздает складскую операцию
        /// - пересоздает связанные сч-фактуры
        /// - перепроводит БУ и НУ
        /// </summary>
        Task ProvideAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);
        
        /// <summary>
        /// Перепроводин документ:
        /// - перепроводит БУ и НУ
        /// - перепроводит БУ и НУ связанных платежей
        /// </summary>
        Task ProvidePostingsOnlyAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Удаляет накладную по Id
        /// </summary>
        Task DeleteByIdAsync(int firmId, int userId, int id);

        /// <summary>
        /// Удаляет накладные по списку DocumentBaseId
        /// </summary>
        Task DeleteByBaseIdAsync(int firmId, int userId, long baseId);
        
        /// <summary>
        /// Возвращает накладные c позициями по списку DocumentBaseId
        /// </summary>
        Task<List<WaybillDto>> GetByBaseIdsAsync(int firmId, int userId,
            IReadOnlyCollection<long> baseIds,
            HttpQuerySetting querySettings = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Устанавливает склад в накладной с указанным DocumentBaseId (покупки)
        /// </summary>
        Task SetStockAsync(int firmId, int userId, long documentBaseId, long stockId);

        /// <summary>
        ///  Возвращает поставки (покупки, для склада)
        /// </summary>
        Task<List<PurchaseInfoResponseDto>> GetLastPurchaseInfoAsync(int firmId, int userId, List<PurchaseInfoRequestDto> purchaseInfoRequests);
    }
}
