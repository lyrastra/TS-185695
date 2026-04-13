using System;
using Moedelo.Common.Types;
using System.Threading.Tasks;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.KudirOsno;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy
{
    public interface IKudirOsnoApiClient
    {
        /// <summary>
        /// Получение зарплатных расходов за период (как правило, год) для формирования КУДИР ИП-ОСНО 
        /// </summary>
        Task<KudirIpOsnoDataDto> GetByPeriodAsync(FirmId firmId, UserId userId, DateTime startDate, DateTime endDate);
    }
}
