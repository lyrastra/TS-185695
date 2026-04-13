using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Access;

namespace Moedelo.Authorization.Dto;

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
    /// </summary>
    public IReadOnlyCollection<AccessRule> TariffRules { get; set; }
}