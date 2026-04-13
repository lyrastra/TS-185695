using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy
{
    /// <summary>
    /// Клиент для работы с складами.
    /// </summary>
    public interface IStockApiClient
    {
        /// <summary>
        /// source: https://github.com/moedelo/md-commonV2/blob/master/src/clients/stock/Moedelo.StockV2.Client/Stocks/IStockClient.cs#L12
        /// </summary>
        Task<StockDto> GetMainAsync(FirmId firmId, UserId userId);
        
        /// <summary>
        /// source: https://github.com/moedelo/md-commonV2/blob/master/src/clients/stock/Moedelo.StockV2.Client/Stocks/IStockClient.cs#L17 
        /// </summary>
        Task<StockDto> GetAsync(FirmId firmId, UserId userId, long stockId);

        /// <summary>
        /// source: https://github.com/moedelo/md-commonV2/blob/master/src/clients/stock/Moedelo.StockV2.Client/Stocks/IStockClient.cs#L23
        /// </summary>
        Task<List<StockDto>> GetAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> stockIds);

        /// <summary>
        /// Проверить имеются ли связанные с сотрудником складские операции
        /// source: https://github.com/moedelo/md-commonV2/blob/master/src/clients/stock/Moedelo.StockV2.Client/Stocks/IStockClient.cs#L49
        /// </summary>
        Task<bool> ExistsWorkerDependenciesAsync(int firmId, int userId, int workerId, long? subcontoId);
    }
}