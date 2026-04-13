using System.Collections.Generic;

namespace Moedelo.Common.AccessRules.Models
{
    internal sealed class TariffsAndRoles
    {
        public IReadOnlyDictionary<int, TariffInfo> Tariffs { get; set; }

        public IReadOnlyDictionary<int, RoleInfo> Roles { get; set; }
    }
}
