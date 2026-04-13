using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.StockV2.Dto.Stocks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.StockV2.Client.Stocks
{
    public interface ICommissionAgentStockClient : IDI
    {
        /// <summary>
        /// Возвращает список складов комиссионеров
        /// </summary>
        Task<List<StockDto>> GetAsync(int firmId, int userId);

        /// <summary>
        /// Создаёт склад комиссионера
        /// </summary>
        Task<long> CreateAsync(int firmId, int userId, string name);

        /// <summary>
        /// Переименовывает склад комиссионера
        /// </summary>
        Task<long> RenameAsync(int firmId, int userId, long stockId, string name);

        /// <summary>
        /// Проверяет возможность удаления склада комиссионера
        /// </summary>
        Task<bool> CanDeleteAsync(int firmId, int userId, long stockId);

        /// <summary>
        /// Удаляет склад комиссионера
        /// </summary>
        Task DeleteAsync(int firmId, int userId, long stockId);
    }
}