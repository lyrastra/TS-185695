using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.PayrollV2.Dto.Funds;

namespace Moedelo.PayrollV2.Client.Funds
{
    public interface IFundsClient : IDI
    {
        /// <summary>
        /// Получить начисленные взносы в фонды за период
        /// (Период должен быть в пределах одного года)
        /// </summary>
        Task<decimal> GetAssessedAsync(int firmId, int userId, DateTime startDate, DateTime endDate);

        Task<List<FundChargesSummaryDto>> GetAssessedToPfrAsync(int firmId, int userId, DateTime startDate, DateTime endDate);

        Task<decimal> GetEmployeesChargeAsync(int firmId, int userId, DateTime startDate, DateTime endDate);
    }
}
