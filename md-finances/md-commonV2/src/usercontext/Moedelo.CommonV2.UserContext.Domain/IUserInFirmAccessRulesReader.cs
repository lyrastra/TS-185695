using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Access;

namespace Moedelo.CommonV2.UserContext.Domain;

/// <summary>
/// Сервис для чтения прав пользователей в фирмах
/// </summary>
public interface IUserInFirmAccessRulesReader
{
    /// <summary>
    /// Возвращает список прав по паре { фирма, пользователь }
    /// </summary>
    Task<ISet<AccessRule>> GetAsync(int firmId, int userId);
}