using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.StockV2.Dto.Stocks;

namespace Moedelo.StockV2.Client.Stocks
{
    public interface IUnfinishedManufacturingClient
    {
        /// <summary>
        /// Сохраняет остатки в незавершенном производстве 
        /// </summary>    
        Task SaveAsync(int firmId, int userId, UnfinishedManufacturingSaveRequestDto dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получает остатки в незавершенном производстве
        /// </summary>
        Task<List<UnfinishedManufacturingItemDto>> GetByDateAsync(int firmId, int userId, DateTime date, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получает остатки в незавершенном производстве
        /// </summary>
        Task<List<UnfinishedManufacturingItemDto>> GetAsync(int firmId, int userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Проверяет наличие остатков в незавершенном производстве для подразделения
        /// </summary>
        Task<bool> ExistsForDivisionAsync(int firmId, int divisionId, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Удаляет остатки в незавершенном производстве по ИД
        /// </summary>
        Task DeleteByIdsAsync(int firmId, int userId, IReadOnlyCollection<int> ids, CancellationToken cancellationToken = default);
    }
}
