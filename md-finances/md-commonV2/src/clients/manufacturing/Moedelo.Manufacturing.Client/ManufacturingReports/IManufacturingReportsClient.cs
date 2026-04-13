using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Manufacturing.Dto.ManufacturingReports;

namespace Moedelo.Manufacturing.Client.ManufacturingReports
{
    public interface IManufacturingReportsClient : IDI
    {
        Task<List<ManufacturingReportDto>> GetByCriterionAsync(int firmId, int userId, ManufacturingReportRequestDto request);
        
        Task<List<ManufacturingReportFullDto>> GetManufacturingReportsByBaseIds(int firmId, int userId, IReadOnlyCollection<long> ids);

        Task<List<ManufacturingReportFullDto>> GetManufacturingReportsByPeriodAsync(int firmId, DateTime? startDate,
            DateTime? endDate);

        Task<bool> ExistsForDivisionAsync(int firmId, int divisionId);

        /// <summary>
        /// Возвращает список товаров, содержащихся хотя-бы в одном отчёте о выпуске готовой продукции как сырье (для заданной фирмы)
        /// </summary>
        Task<List<long>> GetProductIdsInManufacturedProductReportsAsync(int firmId, int userId, ManufacturedProductReportsProductIdsListRequest request);
    }
}