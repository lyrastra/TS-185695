using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.CommonV2.UserContext.Domain.AuthorizationContext;

namespace Moedelo.CommonV2.UserContext;

internal sealed class RolesCacheData
{
    internal RolesCacheData(IEnumerable<RoleInfo> roles, DateTime validUntil)
    {
        List = roles.ToArray();
        Map = List.ToDictionary(role => role.Id);
        ValidUntil = validUntil;
    }

    public RoleInfo[] List { get; }
    public IReadOnlyDictionary<int, RoleInfo> Map { get; }
    public DateTime ValidUntil { get; }
}