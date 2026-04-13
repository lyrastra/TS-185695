using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Ndfl3Validator;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.Ndfl3Validator
{
    /// <summary>
    /// Клиент чтение валидности данных для мастера авансовых платежей ип-осно
    /// </summary>
    public interface INdfl3ValidatorApiClient : IDI
    {
        Task<IReadOnlyCollection<StockProductNegativeBalanceDto>> GetNegativeStockBalancesAsync(int firmId, int userId, DateTime startDate, DateTime endDate);
        Task<int> GetUncoveredPaymentsCountAsync(int firmId, int userId, DateTime startDate, DateTime endDate);
    }
}
