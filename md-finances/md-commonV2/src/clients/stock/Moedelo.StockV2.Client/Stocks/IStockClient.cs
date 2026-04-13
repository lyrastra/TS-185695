using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.StockV2.Dto.Stocks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.StockV2.Client.Stocks
{
    public interface IStockClient : IDI
    {
        /// <summary>
        /// Возвращает основной склад
        /// </summary>
        Task<StockDto> GetMainStockAsync(int firmId, int userId);

        /// <summary>
        /// Возвращает склад с указанным Id
        /// Возвращет null, если склад с таким Id не существует
        /// </summary>
        Task<StockDto> GetAsync(int firmId, int userId, long stockId);

        /// <summary>
        /// Возвращает список складов с указанными Id
        /// </summary>
        Task<List<StockDto>> GetAsync(int firmId, int userId, IReadOnlyCollection<long> stockIds);

        /// <summary>
        /// Возвращает список складов с указанными Id
        /// </summary>
        Task<List<StockDto>> GetAllAsync(int firmId, int userId);

        /// <summary>
        /// Сохранить склад
        /// </summary>
        Task<long?> SaveAsync(int firmId, int userId, StockDto dto);

        /// <summary>
        /// Обновить склад
        /// </summary>
        Task UpdateAsync(int firmId, int userId, StockUpdateDto dto);

        /// <summary>
        /// Создать склад "по умолчанию", если его ещё нет
        /// </summary>
        Task<long> CreateDefaultAsync(int firmId, int userId);

        /// <summary>
        /// Проверить имеются ли связанные с сотрудником складские операции
        /// </summary>
        Task<bool> ExistsWorkerDependenciesAsync(int firmId, int userId, int workerId, long? subcontoId);

        /// <summary>
        /// Проверить закрыт ли год
        /// </summary>
        Task<bool> IsYearClosedAsync(int firmId, int userId, int year);

        /// <summary>
        /// Возвращает список складов с фильтрацией (автокомплит)
        /// </summary>
        Task<List<StockDto>> GetStocksAsync(int firmId, int userId, int count, string query);

        /// <summary>
        /// Возвращает индикатор наличия доступа к складу.
        /// </summary>
        Task<bool> HasAccessAsync(int firmId, int userId);
    }
}