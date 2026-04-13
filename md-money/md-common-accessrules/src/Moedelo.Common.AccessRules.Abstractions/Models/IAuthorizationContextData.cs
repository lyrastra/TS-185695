using System.Collections.Generic;

namespace Moedelo.Common.AccessRules.Abstractions.Models
{
    public interface IAuthorizationContextData
    {
        /// <summary>
        /// идентификатор пользователя, для которого расчитан контекст
        /// </summary>
        int UserId { get; }

        /// <summary>
        /// идентификатор фирмы, для которой расчитан контекст
        /// </summary>
        int FirmId { get; }

        /// <summary>
        /// идентификатор роли, которую исполняет пользователь в данной фирме
        /// </summary>
        int RoleId { get; }

        /// <summary>
        /// права пользователя в рамках роли на данную фирму
        /// </summary>
        IReadOnlyCollection<AccessRule> RoleRules { get; }

        /// <summary>
        /// все права доступные при работе с данной фирмой, согласно действующим оплатам
        /// note: можно объединить с RoleRules путём выделения группы прав, которые не должны срезаться ролями (есть такие)
        /// </summary>
        IReadOnlyCollection<AccessRule> TariffRules { get; }
    }
}