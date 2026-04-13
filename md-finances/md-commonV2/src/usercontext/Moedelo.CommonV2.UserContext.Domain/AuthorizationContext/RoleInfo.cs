using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Access;

namespace Moedelo.CommonV2.UserContext.Domain.AuthorizationContext
{
    public sealed class RoleInfo
    {
        public int Id { get; set; }

        public string RoleCode { get; set; }

        public string Name { get; set; }

        public HashSet<AccessRule> AccessRules { get; set; }
    }
}
