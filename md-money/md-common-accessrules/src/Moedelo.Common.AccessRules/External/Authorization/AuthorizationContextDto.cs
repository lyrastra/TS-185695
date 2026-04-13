using System.Collections.Generic;
using Moedelo.Common.AccessRules.Abstractions;

namespace Moedelo.Common.AccessRules.External.Authorization
{
    public class AuthorizationContextDto
    {
        public const int InvalidRoleId = 0;
        
        /// <summary>
        /// идентификатор пользователя, для которого расчитан контекст
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// идентификатор фирмы, для которой расчитан контекст
        /// </summary>
        public int FirmId { get; set; }

        /// <summary>
        /// идентификатор роли, которую исполняет пользователь в данной фирме
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// права пользователя в рамках роли на данную фирму
        /// </summary>
        public IReadOnlyCollection<AccessRule> RoleRules { get; set; }

        /// <summary>
        /// все права доступные при работе с данной фирмой, согласно действующим оплатам
        /// note: можно объединить с RoleRules путём выделения группы прав, которые не должны срезаться ролями (есть такие)
        /// </summary>
        public IReadOnlyCollection<AccessRule> TariffRules { get; set; }
    }
}