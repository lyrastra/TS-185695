using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.ClosedPeriod
{
    public interface IClosedPeriodReader
    {
        /// <summary>
        /// Либо дата ввода остатков
        /// Либо последняя дата закрытого периода
        /// Либо дата регистрации
        /// </summary>
        Task<DateTime> GetLastClosedDateAsync();

        /// <summary>
        /// Последняя дата закрытого периода
        /// </summary>
        Task<DateTime?> GetLastClosedPeriodDateAsync();

        /// <summary>
        /// Дата ввода остатков
        /// </summary>
        Task<DateTime?> GetBalancesDateAsync();
    }
}
