using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.StockOperations;
using Moedelo.Stock.Enums;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy
{
    /// <summary>
    /// source: https://github.com/moedelo/md-commonV2/blob/038b6a48ed3b6f2f252126264c19a8f4caa8fa8d/src/clients/stock/Moedelo.StockV2.Client/Operations/IStockOperationClient.cs#L11
    /// </summary>
    public interface IStockOperationApiClient
    {
        /// <summary>
        /// Сохраняет складскую операцию
        /// </summary>
        Task SaveOrUpdateAsync(FirmId firmId, UserId userId, StockOperationDto operation);
        
        /// <summary>
        /// Создает новую складскую операцию (не поддерживает требование-накладные: неполная модель позиций)
        /// </summary>
        Task<long> CreateAsync(FirmId firmId, UserId userId, StockOperationCreateDto operation);

        /// <summary>
        /// Сохраняет список складских операций
        /// </summary>
        Task SaveOrUpdateAsync(FirmId firmId, UserId userId, IReadOnlyCollection<StockOperationDto> operations);
        
        /// <summary>
        /// Удаляет складскую операцию по DocumentBaseId документа-основания
        /// </summary>
        Task DeleteBySourceDocumentIdAsync(FirmId firmId, UserId userId, long baseId);

        /// <summary>
        /// Удаляет складские операции по списку DocumentBaseId документов
        /// </summary>
        Task DeleteBySourceDocumentIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);
        
        /// <summary>
        /// Получает складские операции по списку BaseId документов-источников
        /// </summary>
        Task<List<StockOperationDto>> GetStockOperationsBySourceDocumentIdsAsync(int firmId, int userId, IReadOnlyCollection<long> sourceDocumentIds);
        
        /// <summary>
        /// Рассчитывает себестоимость переданной операции вместе с остальными операциями этого месяца
        /// возвращает эту же операцию с рассчитанной себестоимостью. требуется для документов прошлого периода
        /// </summary>
        Task<StockOperationDto> CalculateSelfCostForOperationAsync(int firmId, int userId, StockOperationDto operationDto);
        
        /// <summary>
        /// Возвращает список операций заданного типа
        /// </summary>
        Task<List<StockOperationDto>> GetOperationsByTypeAsync(int firmId, int userId, StockOperationTypeEnum type);
    }
}