using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.Context;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.CommonV2.UserContext.Domain.BillingContext;
using Moedelo.CommonV2.UserContext.Domain.ExtraData;

namespace Moedelo.CommonV2.UserContext.Domain;

public interface IUserContext : IDI
{
    /// <summary>
    /// Идентификатор фирмы
    /// </summary>
    int FirmId { get; }

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    int UserId { get; }
        
    /// <summary>
    /// Признак того, что контекст авторизован (либо UserId > 0, либо FirmId > 0)
    /// </summary>
    bool IsAuthorized { get; }

    /// <summary>
    /// Получить идентификатор роли пользователя
    /// </summary>
    /// <returns></returns>
    Task<int> GetRoleIdAsync();

    /// <summary>
    /// Получить данные о тарифе фирмы
    /// </summary>
    Task<IFirmBillingContextData> GetBillingContextDataAsync();

    /// <summary>
    /// Получить дополнительные данные (требуют загрузки)
    /// </summary>
    Task<IUserContextExtraData> GetContextExtraDataAsync();

    IAuditContext GetAuditContext();

    /// <summary>
    /// Инвалидировать локальный кэш данных контекста (все данные будут перезагружены при следующем обращении)
    /// </summary>
    void Invalidate();

    /// <summary>
    /// Проверить, доступны ли все указанные права пользователю в рамках исполняемой им роли в данной фирме
    /// </summary>
    /// <returns>true - все права доступны, false - хотя бы одно право недоступно</returns>
    Task<bool> HasAllRuleAsync(params AccessRule[] accessRules);

    /// <summary>
    /// проверить, доступно указанное право пользователю в рамках исполняемой им роли в данной фирме
    /// </summary>
    /// <returns>true - право доступно, false - право недоступно</returns>
    Task<bool> HasAllRuleAsync(AccessRule accessRule);

    /// <summary>
    /// проверить, доступно ли хоть одно из указанных прав пользователю в рамках исполняемой им роли в данной фирме
    /// </summary>
    /// <returns>true - доступно хотя бы одно право, false - ни одно право недоступно</returns>
    Task<bool> HasAnyRuleAsync(params AccessRule[] accessRules);
        
    /// <summary>
    /// проверить, доступно указанное право пользователю в рамках исполняемой им роли в данной фирме
    /// </summary>
    /// <returns>true - право доступно, false - право недоступно</returns>
    Task<bool> HasAnyRuleAsync(AccessRule accessRule);

    /// <summary>
    /// проверить, доступно ли хоть одно из указанных прав в рамках действующего тарифа в данной фирме
    /// (т.е. без учёта ограничений, накладываемых ролью пользователя) 
    /// </summary>
    /// <returns>true - доступно хотя бы одно право, false - ни одно право недоступно</returns>
    Task<bool> HasAnyTariffRuleAsync(params AccessRule[] accessRules);

    /// <summary>
    /// проверить, доступны ли указанные права пользователю в рамках исполняемой им роли в данной фирме
    /// </summary>
    /// <returns>список доступных пользователю прав из числа переданных в качестве аргумента функции</returns>
    Task<List<AccessRule>> GetGrantedRulesAsync(params AccessRule[] rulesToCheck);
        
    /// <summary>
    /// все права, доступные пользователю в рамках исполняемой им роли в данной фирме
    /// </summary>
    /// <returns>список доступных пользователю прав</returns>
    Task<IReadOnlyCollection<AccessRule>> GetUserRulesAsync();
}
