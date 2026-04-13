using System;
using System.Threading.Tasks;

namespace Moedelo.Billing.Abstractions.AutoBilling;

public interface IAutoBillingExtraDataApiClient
{
    Task<int> GetAverageTurnoverAsync(int firmId, DateTime startDate, DateTime endDate);
    Task<int> GetWorkersCountAsync(int firmId, DateTime startDate, DateTime endDate);
    Task<int> GetWorkersCountOnDateAsync(int firmId, DateTime date);
    Task<int> GetAverageWorkersCountAsync(int firmId, DateTime startDate, DateTime endDate);
}
