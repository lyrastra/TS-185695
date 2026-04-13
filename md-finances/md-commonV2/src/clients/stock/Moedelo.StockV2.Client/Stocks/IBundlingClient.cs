using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.StockV2.Dto.Stocks;

namespace Moedelo.StockV2.Client.Stocks
{
    public interface IBundlingClient : IDI
    {
        Task<List<BundlingDto>> GetForPeriodAsync(int firmId, int userId, DateTime startDate, DateTime endDate);
    }
}