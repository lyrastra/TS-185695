using Moedelo.Common.Types;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy
{
    /// <summary>
    /// Клиент для работы с складами комиссионеров
    /// </summary>
    public interface ICommissionAgentStockApiClient
    {
        /// <summary>
        /// Возвращает список складов комиссионеров
        /// </summary>
        Task<List<StockDto>> GetAsync(FirmId firmId, UserId userId);

        /// <summary>
        /// Создаёт склад комиссионера
        /// </summary>
        Task<long> CreateAsync(FirmId firmId, UserId userId, string name);

        /// <summary>
        /// Переименовывает склад комиссионера
        /// </summary>
        Task RenameAsync(FirmId firmId, UserId userId, long stockId, string name);

        /// <summary>
        /// Проверяет возможность удаления склада комиссионера
        /// </summary>
        Task<bool> CanDeleteAsync(FirmId firmId, UserId userId, long stockId);

        /// <summary>
        /// Удаляет склад комиссионера
        /// </summary>
        Task DeleteAsync(FirmId firmId, UserId userId, long stockId);
    }
}