using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Access;

namespace Moedelo.CommonV2.UserContext.Domain.AuthorizationContext
{
    public sealed class TariffInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProductPlatform { get; set; }

        public string ProductGroup { get; set; }

        public HashSet<AccessRule> AccessRules { get; set; }
    }
}
