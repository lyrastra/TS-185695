using System;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.FifoSelfCost;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy
{
    public interface IFifoSelfCostApiClient
    {
        /// <summary>
        /// Получение данных склада, необходимых для расчета с/с
        /// </summary>
        /// <param name="firmId">Фирма</param>
        /// <param name="userId">Пользователь</param>
        /// <param name="beforeDate">Дата на которую рассчитывать остатки (не включительно)</param>
        /// <param name="balancesOperationDate">Дата ввода остатков</param>
        Task<FifoSelfCostProductSummariesDto> GetProductSummariesAsync(
            FirmId firmId, 
            UserId userId,
            DateTime beforeDate,
            DateTime? balancesOperationDate = null);

        /// <summary>
        /// Возвращает остатки, введенные пользователем в реквизитах, для расчета себестоимости по ФИФО
        /// </summary>
        Task<RemainsSelfCostDto> GetSelfCostRemainsAsync(
            FirmId firmId,
            UserId userId);
    }
}