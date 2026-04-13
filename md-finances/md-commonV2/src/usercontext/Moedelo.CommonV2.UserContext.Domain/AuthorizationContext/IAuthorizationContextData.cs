using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Access;

namespace Moedelo.CommonV2.UserContext.Domain.AuthorizationContext
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
        /// права пользователя в рамках его роли на данную фирму
        /// </summary>
        IReadOnlyCollection<AccessRule> UserRules { get; }

        /// <summary>
        /// все права доступные при работе с данной фирмой, согласно действующим оплатам
        /// </summary>
        IReadOnlyCollection<AccessRule> TariffRules { get; }
    }
}