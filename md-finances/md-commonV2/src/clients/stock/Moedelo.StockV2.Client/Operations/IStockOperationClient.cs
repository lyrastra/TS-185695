using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Stocks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.StockV2.Dto.Operations;
using Moedelo.StockV2.Dto.Operations.ProductMerge;
using Moedelo.StockV2.Dto.SelfCost;

namespace Moedelo.StockV2.Client.Operations
{
    public interface IStockOperationClient : IDI
    {
        /// <summary>
        /// Создает новую складскую операцию
        /// </summary>
        /// <returns>Идентификатор складской операции</returns>
        Task<long> CreateAsync(int firmId, int userId, StockOperationCreateDto operation);
        
        /// <summary>
        /// Сохраняет складскую операцию
        /// </summary>
        Task SaveOrUpdateAsync(int firmId, int userId, StockOperationDto operation);

        /// <summary>
        /// Сохраняет список складских операций
        /// </summary>
        Task SaveOrUpdateAsync(int firmId, int userId, IReadOnlyCollection<StockOperationDto> operations);

        /// <summary>
        /// Удаляет складскую операцию по DocumentBaseId документа-основания
        /// </summary>
        Task DeleteBySourceDocumentIdAsync(int firmId, int userId, long baseId);

        /// <summary>
        /// Удаляет складские операции по списку DocumentBaseId документов
        /// </summary>
        Task DeleteBySourceDocumentIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Удаляет складские операции по списку Id
        /// </summary>
        Task DeleteByIdsAsync(int firmId, int userId, IReadOnlyCollection<long> ids);

        /// <summary>
        /// Получает складские операции по списку BaseId документов-источников
        /// </summary>
        Task<List<StockOperationDto>> GetStockOperationsBySourceDocumentIdsAsync(int firmId, int userId, IReadOnlyCollection<long> sourceDocumentIds);

        /// <summary>
        /// Возвращает список операций за указанный период, для которых рассчитана себестоимость
        /// </summary>
        Task<SelfCostResultDto> GetStockOperationsWithSelfCostForPeriodAsync(int firmId, int userId, DateTime startDate,
            DateTime endDate);

        /// <summary>
        /// Возвращает список операций за указанный период, для которых рассчитана себестоимость
        /// </summary>
        Task<SelfCostResultDto> GetStockOperationsWithSelfCostForPeriodAsync(int firmId, int userId,
            StockOperationRequestDto request);

        /// <summary>
        /// Возвращает список операций за указанный период, для которых рассчитана себестоимость. С учетом забыт
        /// </summary>
        Task<SelfCostResultDto> GetStockOperationsWithSelfCostForPeriodWithFogottenAsync(int firmId, int userId, DateTime startDate,
            DateTime endDate, bool checkNegativeOperations = true);

        /// <summary>
        /// Обновляет себестоимость в складских операциях
        /// </summary>
        Task UpdateSelfCostAsync(int firmId, int userId, IReadOnlyCollection<SelfCostUpdateRequestDto> updateRequest);

        /// <summary>
        /// Рассчитывает себестоимость переданной операции вместе с остальными операциями этого месяца
        /// возвращает эту же операцию с рассчитанной себестоимостью. требуется для документов прошлого периода
        /// </summary>
        ///
        Task<StockOperationDto> CalculateSelfCostForOperationAsync(int firmId, int userId, StockOperationDto operationDto);

        /// <summary>
        /// Возвращает список операций по их Id
        /// </summary>
        Task<List<StockOperationDto>> GetOperationsByIdsAsync(int firmId, int userId, IReadOnlyCollection<long> ids);

        /// <summary>
        /// Возвращает список операций заданного типа
        /// </summary>
        Task<List<StockOperationDto>> GetOperationsByTypeAsync(int firmId, int userId, StockOperationTypeEnum type);

        /// <summary>
        /// Возвращает список операций над товарами в сокращенном виде, которые существуют на указанную дату
        /// </summary>
        Task<List<StockOperationPlaneDto>> GetPlaneStockOperationsOnDateAsync(int firmId, int userId, DateTime date);

        /// <summary>
        /// Возвращает список операций по периоду
        /// </summary>
        Task<List<StockOperationDto>> GetOperationsByPeriodAsync(int firmId, int userId, DateTime period, DateTime periodEndDate);

        /// <summary>
        /// Возвращает тип складской операций по коду
        /// </summary>
        Task<StockOperationTypeDto> GetStockOperationTypeByCodeAsync(int firmId, int userId, StockOperationTypeEnum stockOperationTypeCode);

        /// <summary>
        /// Возвращает тип складской операций по id
        /// </summary>
        Task<StockOperationTypeDto> GetStockOperationTypeByIdAsync(int firmId, int userId, int id);

        /// <summary>
        /// Получает себестоимость остатков по товарам, переданным на склад комиссионера, и проданным с него,
        /// на начало указанного периода и на конец.
        /// </summary>
        Task<IReadOnlyCollection<CommissionAgentProductRemainsDto>> GetCommissionAgentProductRemainsAsync(int firmId, int userId, DateTime startDate, DateTime endDate);
    }
}