using Moedelo.AstralV3.Client.DAO.DbObjects;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.AstralV3.Client.AstralInteractionsLogger.Interfaces
{
    /// <summary>
    /// Интерфейс для получения настроек логирования для различных методов.
    /// </summary>
    public interface IInteractionMethodsSettingsRepository : IDI
    {
        /// <summary>
        /// Метод получает информацию о настройках логирования для каждого из переданных методов. Имена должны быть уникальными.
        /// Если метода ещё нет в репозитории, то добавляет его с настройками по-умолчанию и возвращает эти настройки для него.
        /// </summary>
        Task<Dictionary<string, AstralInteractionMethod>> GetSettings(List<string> methodNames);
    };
}
