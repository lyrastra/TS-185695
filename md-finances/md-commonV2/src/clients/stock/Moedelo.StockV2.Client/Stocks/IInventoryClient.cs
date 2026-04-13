using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.StockV2.Dto.Inventories;

namespace Moedelo.StockV2.Client.Stocks
{
    public interface IInventoryClient : IDI
    {
        /// <summary>
        /// Возвращает документы инвентаризаций за перод
        /// </summary>
        Task<List<InventoryDto>> GetByPeriodAsync(int firmId, int userId, DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// Возвращает документы инвентаризаций по списку BaseId
        /// </summary>
        Task<List<InventoryDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);
    }
}