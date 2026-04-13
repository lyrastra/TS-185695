using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.ClosedPeriods;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.ClosedPeriods
{
    /// <summary>
    /// Клиент настроек МЗМ
    /// </summary>
    public interface IClosingPeriodSettingsApiClient : IDI
    {
        /// <summary>
        /// Возвращает настройки МЗМ для фирмы
        /// </summary>
        Task<ClosingPeriodSettingsDto> GetAsync(int firmId, int userId);
    }
}