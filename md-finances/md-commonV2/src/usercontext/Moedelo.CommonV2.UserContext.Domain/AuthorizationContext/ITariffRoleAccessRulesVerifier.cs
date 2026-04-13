using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Access;

namespace Moedelo.CommonV2.UserContext.Domain.AuthorizationContext
{
    /// <summary>
    /// Код, использующий этот контракт, скорее всего уже работает с ошибками,
    /// и чем дальше, тем больше будет приводить к ошибкам
    /// ПРИЧИНА: не поддерживается новый биллинг (большая часть клиентов находится на платежах из нового биллинга) 
    /// </summary>
    [Obsolete("Не работает для нового биллинга. И не будет работать (там нет тарифов с идентификаторами)")]
    public interface ITariffRoleAccessRulesVerifier
    {
        Task<bool> HasAllRuleAsync(TariffRolePair pair, AccessRule[] accessRules);

        Task<bool> HasAnyRuleAsync(TariffRolePair pair, AccessRule[] accessRules);

        Task<bool> HasAnyRuleInTariffAsync(int tariffId, AccessRule[] accessRules);

        Task<HashSet<AccessRule>> GetTariffRoleRulesAsync(TariffRolePair tariffRolePair);
    }
}