using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Tools;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Tariffs.Dto.TariffLimits;

namespace Moedelo.Tariffs.Client.TariffLimits
{
    /// <summary> Клиент для ограничений использования тарифов </summary>
    public interface ITariffLimitsClient : IDI
    {
        /// <summary>
        /// Получить ограничение тарифа по типу
        /// </summary>
        /// <param name="tariffId">Id тарифа</param>
        /// <param name="toolType">Тип ограничения</param>
        /// <returns></returns>
        Task<TariffLimitDto> GetAsync(int tariffId, ToolType toolType);
    }
}
