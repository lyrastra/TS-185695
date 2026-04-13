using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Estate.Client.FixedAsetInvestments.Dto;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Estate.Client.FixedAsetInvestments
{
    /// <summary>
    /// Клиент для работы с вложениями во внеоборотные активы (ВА)
    /// </summary>
    public interface IFixedAssetInvestmentClientClient : IDI
    {
        /// <summary>
        /// Возвращает ВА по списку DocumentBaseId
        /// </summary>
        Task<FixedAssetInvestmentDto> GetByBaseIdAsync(int firmId, int userId, long fixedAssetBaseId);

        /// <summary>
        /// Возвращает ВА по списку Id
        /// </summary>
        Task<List<FixedAssetInvestmentDto>> GetByIdsAsync(int firmId, int userId, IReadOnlyCollection<long> ids);

        /// <summary>
        /// Обновляет ВА при сохранении остатков.
        /// Если передать пустой список, удалит ВА, созданные из остатков. 
        /// </summary>
        Task UpdateFromBalancesAsync(int firmId, int userId, IReadOnlyCollection<FixedAssetInvestmentBalanceSaveDto> balances);
    }
}