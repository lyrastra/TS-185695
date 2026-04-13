using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.Dto.ProductsTrace;

namespace Moedelo.Docs.Client.ProductsTrace
{
    public interface IProductTraceApiClient
    {
        /// <summary>
        /// Возвращает словарь прослеживания товаров по идентификаторам документов
        /// </summary>
        Task<Dictionary<long, List<ProductTraceResponseDto>>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds);

        /// <summary>
        /// Возвращает прослеживания товаров по идентификатору документа
        /// </summary>
        Task<IReadOnlyCollection<ProductTraceResponseDto>> GetByBaseIdAsync(int firmId, int userId, long documentBaseId);

        /// <summary>
        /// Удаляет прослеживания товаров по идентификаторам документов
        /// </summary>
        Task DeleteByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds);

        /// <summary>
        /// Удаляет прослеживания товаров по идентификатору документа
        /// </summary>
        Task DeleteByBaseIdAsync(int firmId, int userId, long documentBaseId);

        /// <summary>
        /// Сохраняет прослеживания товаров
        /// </summary>
        Task SaveAsync(int firmId, int userId, IReadOnlyCollection<ProductTraceSaveDto> dtos);

        /// <summary>
        /// Проверяет имеются ли прослеживания конкретного товара
        /// </summary>
        Task<bool> ProductHasTraceAsync(int firmId, int userId, long productId);
    }
}