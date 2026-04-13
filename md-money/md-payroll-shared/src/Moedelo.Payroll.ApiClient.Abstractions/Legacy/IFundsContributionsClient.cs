using System;
using System.Threading.Tasks;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.FundsContributions;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy
{
    public interface IFundsContributionsClient
    {
        /// <summary>
        /// Получение списка взносов в фонды за работников в рамках одного года
        /// </summary>
        Task<FundsContributionsDto> GetByPeriodAsync(int firmId, int userId, DateTime startDate, DateTime endDate);
    }
}
