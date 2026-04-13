using Moedelo.Docs.Dto.RetailRefunds;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Docs.Client.RetailRefunds
{
    public interface IRetailRefundApiClient : IDI
    {
        Task<long> SaveAsync(int firmId, int userId, RetailRefundDto dto);

        Task<RetailRefundDto> GetByBaseIdAsync(int firmId, int userId, long documentBaseId);

        Task<RetailRefundPageDto> GetPageAsync(int firmId, int userId, RetailRefundPaginationRequestDto request);

        Task<List<RetailRefundDto>> GetByPeriodAsync(int firmId, int userId, DateTime startDate, DateTime endDate, long? stockId = null, CancellationToken cancellationToken = default);

        Task<List<RetailRefundDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        Task DeleteAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        Task DeleteAsync(int firmId, int userId, long retailRefundBaseId);

        Task DeleteWithCashOrdersAsync(int firmId, int userId, long retailRefundBaseId);

        Task<string> GetNextNumberAsync(int firmId, int userId, int? year);
    }
}