using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.StockV2.Dto.Stocks;

namespace Moedelo.StockV2.Client.Stocks
{
    public interface IRequisitionWaybillClient : IDI
    {
        /// <summary>
        /// Возвращает накладную по BaseId
        /// </summary>
        Task<RequisitionWaybillWithStockOperationDto> GetByBaseIdAsync(int firmId, int userId, long documentBaseId);

        /// <summary>
        /// Возвращает накладную по id
        /// </summary>
        Task<RequisitionWaybillWithStockOperationDto> GetByIdAsync(int firmId, int userId, long id);

        /// <summary>
        /// Возвращается список накладных по идентификаторам
        /// </summary>
        Task<List<RequisitionWaybillWithStockOperationDto>> GetByIdsAsync(int firmId, int userId, IReadOnlyList<long> ids);

        Task<List<StockDocumentTaxInfoDto>> CheckForTaxableAsync(int firmId, int userId, IReadOnlyCollection<RequisitionWaybillDocumentDto> requsitionWaybillDtos);

        Task<List<RequisitionWaybillDocumentDto>> GetForPeriodAsync(int firmId, int userId, DateTime startDate, DateTime endDate);

        Task DeleteByBaseIdAsync(int firmId, int userId, long baseId);
    }
}
