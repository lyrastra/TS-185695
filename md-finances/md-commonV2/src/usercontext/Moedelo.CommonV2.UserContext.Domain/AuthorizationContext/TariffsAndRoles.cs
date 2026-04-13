using System.Collections.Generic;

namespace Moedelo.CommonV2.UserContext.Domain.AuthorizationContext
{
    public sealed class TariffsAndRoles
    {
        public IReadOnlyDictionary<int, TariffInfo> Tariffs { get; set; }

        public IReadOnlyDictionary<int, RoleInfo> Roles { get; set; }
    }
}