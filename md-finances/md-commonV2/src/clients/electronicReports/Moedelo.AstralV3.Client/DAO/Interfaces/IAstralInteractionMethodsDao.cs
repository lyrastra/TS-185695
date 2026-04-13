using Moedelo.AstralV3.Client.AstralInteractionsLogger.Enums;
using Moedelo.AstralV3.Client.DAO.DbObjects;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.AstralV3.Client.DAO.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с таблицей AstralInteractionMethods
    /// </summary>
    public interface IAstralInteractionMethodsDao : IDI
    {
        /// <summary>
        /// Возвращает список объектов с информацией о методах по их названиям
        /// </summary>
        Task<List<AstralInteractionMethod>> Get(List<string> methodNames);

        /// <summary>
        /// Возвращает список объектов с информацией о методах по их ключам
        /// </summary>
        Task<List<AstralInteractionMethod>> Get(List<int> ids);

        /// <summary>
        /// Добавляет запись в AstralInteractionMethods и возвращает её Id
        /// </summary>
        Task<int> Add(string methodName, MethodLoggingMode mode);
    }
}
